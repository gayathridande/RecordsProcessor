using System.Reflection;
using System.Web;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using RecordProcessor.Api.IoC;

namespace RecordProcessor.Api
{
    public class WebApiApplication : HttpApplication
    {
        public static readonly string[] RecordPaths =
        {
            "C:\\RecordProcessor\\records_comma.txt",
            "C:\\RecordProcessor\\records_pipe.txt",
            "C:\\RecordProcessor\\records_space.txt",
        };

        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            var container = BuildContainer();
            SetupPostInitDependencies(container);
        }

        protected void SetupPostInitDependencies(IContainer container)
        {
            var newBuilder = new ContainerBuilder();
            newBuilder.RegisterModule(new PostApplyApiModule(container,RecordPaths));
            newBuilder.Update(container);
        }

        protected IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new ApiModule());
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            var container = builder.Build();
            var config = GlobalConfiguration.Configuration;
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            return container;
        }
    }
}
