using System.Globalization;
using System.Threading;
using Autofac;
using RecordProcessor.Application;
using RecordProcessor.Console.IoC;

namespace RecordProcessor.Console
{
    public class Program
    {
        public const int Success = 0;
        public const int Error = -1;

        public static int Main(string[] args)
        {
            ConfigureApplicationDefaults();
            var builder = BuildContainer();
            var result = InitializeAndRunApplication(args, builder);
            return result.Success ? Success : Error;
        }

        private static void ConfigureApplicationDefaults()
        {
            CultureInfo culture = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            culture.DateTimeFormat.ShortDatePattern = "M/d/yyyy";
            culture.DateTimeFormat.LongTimePattern = "";
            Thread.CurrentThread.CurrentCulture = culture;
        }
        
        private static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new ConsoleModule());
            return builder.Build();
        }

        private static RecordsProcessedResult InitializeAndRunApplication(string[] args, IContainer builder)
        {
            RecordsProcessedResult result;
            using (var scope = builder.BeginLifetimeScope())
            {
                var processor = scope.Resolve<IRecordProcessor>();
                result = processor.Run(args);
            }
            return result;
        }

    }
}
