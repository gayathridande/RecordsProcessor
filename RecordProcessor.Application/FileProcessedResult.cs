using System.Collections.Generic;
using RecordProcessor.Application.Domain;

namespace RecordProcessor.Application
{
    public class RecordsProcessedResult
    {
        public bool Success { get; set; }
        public string ErrorMessage { private get; set; }
        public string SuccessMessage { private get; set; }
        public string DisplayMessage { get { return Success ? SuccessMessage : ErrorMessage; } }
        public IEnumerable<Record> Records { get; set; }
    }
}