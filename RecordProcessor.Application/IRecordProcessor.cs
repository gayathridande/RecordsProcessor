namespace RecordProcessor.Application
{
    public interface IRecordProcessor
    {
        RecordsProcessedResult Run(string[] args);
    }
}
