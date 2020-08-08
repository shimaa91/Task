using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Task.Data.Models;

namespace Task.Data.DataContext
{
    public  class DataSeedingIntilization
    {
        private static TaskDbContext _taskDbContext;
        private static UserManager<IdentityUser> _userManager;
        private static IServiceProvider _serviceProvider;

        public static void Seed(TaskDbContext taskDbContext, IServiceProvider serviceProvider)
        {
            _taskDbContext = taskDbContext;
            taskDbContext.Database.EnsureCreated();
            _serviceProvider = serviceProvider;

            var serviceScope = _serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            _userManager = serviceScope.ServiceProvider.GetService<UserManager<IdentityUser>>();

            // call functions

            //SeedApplicationRoles();
            SeedApplicationDefaultUsers();
            SeedApplicationDefaultAdmin();

            // save to the database
            _taskDbContext.SaveChanges();
        }

        private static void SeedApplicationDefaultAdmin()
        {
            var superAdmin = _userManager.FindByNameAsync("admin@info.com");
            if (superAdmin.Result == null)
            {
                var applicationUser = new IdentityUser()
                {
                    EmailConfirmed = true,
                    UserName = "admin@info.com",
                    Email = "admin@info.com",
                    LockoutEnabled = false,
                };

                var result = _userManager.CreateAsync(applicationUser, "Admin@2020");
                if (result.Result.Succeeded)
                {
                    superAdmin = _userManager.FindByEmailAsync("admin@info.com");
                }
            }
        }

        private static void SeedApplicationDefaultUsers()
        {
            var users = _taskDbContext.Users.ToList();
            if (users == null || users.Count == 0)
            {
                var applicationUser8 = new User()
                {
                    FullName = "شيماء  عبدالرحيم   محمود",
                    Birthdate = DateTime.Parse("5-10-1991"),
                    Gender = Enums.Gender.Female

                };
                _taskDbContext.Users.Add(applicationUser8);
                _taskDbContext.SaveChanges();
                var applicationUser1 = new User()
                {
                    FullName = "علياء احمد",
                    Birthdate = DateTime.Parse("5-1-1996"),
                    Gender = Enums.Gender.Female
                };
                _taskDbContext.Users.Add(applicationUser1);
                _taskDbContext.SaveChanges();
                var applicationUser2 = new User()
                {
                    FullName = "احمد",
                    Birthdate = DateTime.Parse("5-5-1981"),
                    Gender = Enums.Gender.Male
                };
                _taskDbContext.Users.Add(applicationUser2);
                _taskDbContext.SaveChanges();
                var applicationUser3 = new User()
                {
                    FullName = "محمود",
                    Birthdate = DateTime.Parse("5-10-1987"),
                    Gender = Enums.Gender.Male
                };
                _taskDbContext.Users.Add(applicationUser3);
                _taskDbContext.SaveChanges();
                var applicationUser4 = new User()
                {
                    FullName = "خالد    أحمد",
                    Birthdate = DateTime.Parse("5-10-1991"),
                    Gender = Enums.Gender.Male
                };
                _taskDbContext.Users.Add(applicationUser4);
                _taskDbContext.SaveChanges();
                var applicationUser5 = new User()
                {
                    FullName = "منار    محمد",
                    Birthdate = DateTime.Parse("5-10-1988"),
                    Gender = Enums.Gender.Female
                };
                _taskDbContext.Users.Add(applicationUser5);
                _taskDbContext.SaveChanges();                
            }
        }
    }
}
