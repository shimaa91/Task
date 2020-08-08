using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Autofac;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Task.Data.DataContext;
using Task.Data.Models;
using Task.Repositories.UserRepositories;
using Task.Services.IdentityUserServices;
using Task.Services;
using Autofac.Extensions.DependencyInjection;
using Task.Repositories.UnitOfWork;
using Microsoft.AspNetCore.Http;
using SimpleInjector;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using SimpleInjector.Integration.AspNetCore.Mvc;
using SimpleInjector.Lifestyles;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Task.API.Controllers;

namespace Task.API
{
    public class SingleCultureProvider : IRequestCultureProvider
    {
        public Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {
            return System.Threading.Tasks.Task.Run(() => new ProviderCultureResult("ar-EG", "ar-EG"));
        }
    }
    public class Startup
    {
        private static Container container;
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public IConfiguration Configuration { get; }

        public static Container Container
        {
            get
            {
                return container;
            }
        }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            container = new Container();
        }



        private UserManager<IdentityUser> userManager;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TaskDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredLength = 5;
            }).AddEntityFrameworkStores<TaskDbContext>().AddDefaultTokenProviders();

            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration["AuthSettings:Audience"],
                    ValidIssuer = Configuration["AuthSettings:Issuer"],
                    RequireExpirationTime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["AuthSettings:Key"])),
                    ValidateIssuerSigningKey = true,
                };
            });

            services.AddCors();

            services.AddScoped<IIdentityUserService, IdentityUserService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddControllers();
            services.AddSingleton<IControllerActivator>(new SimpleInjectorControllerActivator(container));
            IntegrateSimpleInjector(services);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider, TaskDbContext taskDbContext)
        {
            SimpleInjectorInitializer.Initialize(container, Configuration);
            app.UseRequestLocalization();
            var requestOpt = new RequestLocalizationOptions();

            requestOpt.SupportedCultures = new List<CultureInfo> { new CultureInfo("en-US") };
            requestOpt.SupportedUICultures = new List<CultureInfo> { new CultureInfo("en-US") };
            requestOpt.RequestCultureProviders.Clear();
            requestOpt.RequestCultureProviders.Add(new SingleCultureProvider());
            app.UseRequestLocalization(requestOpt);
            container.Register<WeatherForecastController>();
            container.Register<UserController>();
            container.Register<AccountController>();
            container.Verify();


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            DataSeedingIntilization.Seed(taskDbContext, serviceProvider);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseCors(option =>
            {
                option.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
            });
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void IntegrateSimpleInjector(IServiceCollection services)
        {
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSingleton<IControllerActivator>(
                new SimpleInjectorControllerActivator(container));
            services.AddSingleton<IViewComponentActivator>(
                new SimpleInjectorViewComponentActivator(container));
            services.AddSimpleInjector(container);
            services.UseSimpleInjectorAspNetRequestScoping(container);
        }
    }
}
