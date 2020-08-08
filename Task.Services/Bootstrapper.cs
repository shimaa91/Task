using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SimpleInjector;
using Task.Data.DataContext;
using Task.Repositories.UnitOfWork;
using Task.Services.GeneralServices;
using Task.Services.UserServices;

namespace Task.Services
{
    public static class Bootstrapper
    {
        public static void Bootstrap(Container container, IConfiguration configuration)
        {
            container.Register<IUserService, UserService>(container.Options.DefaultScopedLifestyle);
            Task.Repositories.Bootstrapper.Bootstrap(container, configuration);
        }
    }
}
