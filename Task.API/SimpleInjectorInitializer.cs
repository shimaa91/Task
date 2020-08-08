using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using Microsoft.Extensions.Configuration;

namespace Task.API
{
    public static class SimpleInjectorInitializer
    {
        static Container container;
        public static Container Container
        {
            get
            {
                return container;
            }
        }

        /// <summary>Initialize the container and register it as Web API Dependency Resolver.</summary>
        public static void Initialize(Container _container, IConfiguration configuration)
        {
            //container = new Container();
            container = _container;            

            InitializeContainer(container, configuration);
        }

        private static void InitializeContainer(Container container, IConfiguration configuration)
        {
            //Register your services here (remove this line).            

            Task.Services.Bootstrapper.Bootstrap(container, configuration);
        }
    }
}