namespace RecordProcessor.Application.Parsers
{
    public interface IParser<T>
    {
        T Parse(string data);
    }
}