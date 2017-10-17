using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc3;
using CustomerGet.Business.Functions;
using CustomerGet.Controllers;
using System.Net.Http;

namespace CustomerGet
{
    public static class Bootstrapper
    {
        public static void Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            //container.RegisterType<ICustomerDataFactory, TestCustomerDataFactory>();
            container.RegisterType<ICustomerDataFactory, LiveCustomerDataFactory>();
            container.RegisterType<ICustomerServiceApi, CustomerServiceApi>();

            container.RegisterType<IController, HomeController>("Home");

            return container;
        }
    }
}