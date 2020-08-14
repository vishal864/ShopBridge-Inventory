using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using ShopBridge.Service.Interfaces;
using ShopBridge.Service.Service;
using ShopBridge.Repository.Interfaces;
using ShopBridge.Repository;

namespace Newspaper.API.App_Start
{
    public class IOCConfig
    {
        /// <summary>
        /// Configuration IOC
        /// </summary>
        public static void Configure()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<InventoryRepository>().As<IInventoryRepository>().InstancePerRequest();
            builder.RegisterType<InventoryService>().As<IInventoryService>().InstancePerRequest();

            IContainer container = builder.Build();
            var resolver = new AutofacWebApiDependencyResolver(container);

            GlobalConfiguration.Configuration.DependencyResolver = resolver;
        }
    }
}