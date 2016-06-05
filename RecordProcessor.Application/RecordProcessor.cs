using RecordProcessor.Application.Domain;
using RecordProcessor.Application.Validators;

namespace RecordProcessor.Application
{
    public class RecordProcessor : IRecordProcessor
    {
        private readonly IBuilder<Record> _recordBuilder;
        private readonly IValidator<string[]> _validator;
        private readonly IPrinter _printer;

        public RecordProcessor(IBuilder<Record> recordBuilder, IValidator<string[]> validator, IPrinter printer)
        {
            _recordBuilder = recordBuilder;
            _validator = validator;
            _printer = printer;
        }

        public RecordsProcessedResult Run(string[] args)
        {
            var validationResult = _validator.IsValid(args);
            if (!validationResult.IsValid)
            {
                var failedResult = new RecordsProcessedResult{Success = false, ErrorMessage = validationResult.ErrorMessage};
                _printer.Print(failedResult.DisplayMessage);
                return failedResult;
            }

            var records = _recordBuilder.Build(new []{args[0],args[1],args[2]},args[4]);

            var successMessage = string.Join("\n",records);
            var result = new RecordsProcessedResult { Success = true, Records =  records, SuccessMessage = successMessage};
            _printer.Print(result.DisplayMessage);

            return result;
        }
    }
}