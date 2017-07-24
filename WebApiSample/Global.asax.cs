using System.Reflection;
using System.Web;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using WebApiSample.Controllers;
using WebApiSample.Filters;

namespace WebApiSample
{
    public class Global : HttpApplication
    {
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();
            var config = GlobalConfiguration.Configuration;
            // builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterWebApiFilterProvider(config);
           
            builder.RegisterType<ProductsController>().InstancePerRequest();
            builder.RegisterType<ConsoleLogger>().As<ILogger>();
            builder.Register(c => new LoggingActionFilter(c.Resolve<ILogger>()))
                   .AsWebApiActionFilterFor<ProductsController>(c => c.GetProduct(default(int)))
				//   .AsWebApiActionFilterFor<ProductsController>()
				    //  .AsWebApiActionFilterFor<ProductsController>(c => c.GetAllProducts())
																.InstancePerRequest();
            builder.Register(c => new LoggingActionFilter(c.Resolve<ILogger>()))
                 .AsWebApiActionFilterFor<ProductsController>(c => c.GetAllProducts())
                   .InstancePerRequest();
            
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
