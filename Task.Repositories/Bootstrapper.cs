using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Text;
using Task.Data.DataContext;
using Task.Repositories.GenericRepositories;
using Task.Repositories.UnitOfWork;
using Task.Repositories.UserRepositories;


namespace Task.Repositories
{
    public static class Bootstrapper
    {
        public static void Bootstrap(Container container, IConfiguration configuration)
        {

            DbContextOptionsBuilder<TaskDbContext> builder = new DbContextOptionsBuilder<TaskDbContext>();
            builder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            container.Register<IdentityDbContext>(() => new TaskDbContext(builder.Options), container.Options.DefaultScopedLifestyle);

            container.Register<TaskDbContext>(() => new TaskDbContext(builder.Options), container.Options.DefaultScopedLifestyle);

            container.Register<IEnumerable<DbContext>>(() => new List<DbContext>
            {
                new TaskDbContext(builder.Options)
            },
            Lifestyle.Scoped);
            container.Register<IUserRepository, UserRepository>(container.Options.DefaultScopedLifestyle);
            container.Register<IUnitOfWork, UnitOFWork>(container.Options.DefaultScopedLifestyle);


        }
    }
}
