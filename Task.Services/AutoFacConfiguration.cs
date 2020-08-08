using Autofac;
using System;
using System.Collections.Generic;
using System.Text;
using Task.Repositories.UnitOfWork;
using Task.Repositories.UserRepositories;
using Task.Services.GeneralServices;
using Task.Services.UserServices;

namespace Task.Services
{
    public class AutoFacConfiguration : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // base.Load(builder);

            // Register Unit of work service
            builder.RegisterGeneric(typeof(UnitOFWork)).As(typeof(IUnitOfWork));
            // Global

            builder.RegisterType<GeneralService>().As<IGeneralService>();

            #region SecuritycSchema

            // User
            builder.RegisterType<UserService>().As<IUserService>();
            // User Roles
            builder.RegisterType<UserRepository>().As<IUserRepository>();

            #endregion           
        }
    }
}
