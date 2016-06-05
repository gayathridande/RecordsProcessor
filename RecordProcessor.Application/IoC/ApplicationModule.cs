using Autofac;
using RecordProcessor.Application.Domain;
using RecordProcessor.Application.Parsers;
using RecordProcessor.Application.Sorters;
using RecordProcessor.Application.Validators;

namespace RecordProcessor.Application.IoC
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var delimiters = new[] {"|", ",", " "};
            builder.RegisterType<RecordProcessor>().As<IRecordProcessor>();
            builder.RegisterType<ArgumentsValidator>().As<IValidator<string[]>>();
            builder.RegisterType<RecordParser>().As<IParser<Record>>();
            builder.Register(c => new RecordParser(delimiters)).As<IParser<Record>>();
            builder.RegisterType<SortMethodParser>().As<IParser<SortMethod>>();
            builder.RegisterType<RecordsBuilder>().As<IBuilder<Record>>();
            builder.RegisterType<SortStrategyFactory>().As<ISortStrategyFactory>();
            builder.RegisterType<FileHelper>().As<IContentHelper>();
        }
    }
}
