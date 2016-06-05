using Autofac;
using RecordProcessor.Application.IoC;

namespace RecordProcessor.Api.IoC
{
    public class ApiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new ApplicationModule());
        }
    }
}