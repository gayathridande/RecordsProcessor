using System.Linq;
using Autofac;
using RecordProcessor.Application;
using RecordProcessor.Application.Domain;
using RecordProcessor.Application.Repositories;

namespace RecordProcessor.Api.IoC
{
    public class PostApplyApiModule : Module
    {
        private readonly IContainer _container;
        private readonly string[] _recordPaths;

        public PostApplyApiModule(IContainer container, string[] recordPaths)
        {
            _container = container;
            _recordPaths = recordPaths;
        }

        protected override void Load(ContainerBuilder builder)
        {
            var recordBuilder = _container.Resolve<IBuilder<Record>>();
            var results = recordBuilder.Build(_recordPaths, "0");
            builder.Register(c => new InMemoryRecordRepository(results.ToList())).As<IRecordRepository>().SingleInstance();
        }
    }
}