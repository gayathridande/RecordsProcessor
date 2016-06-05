using RecordProcessor.Application;

namespace RecordProcessor.Console
{
    public class ConsolePrinter : IPrinter
    {
        public void Print(string message)
        {
            System.Console.Out.WriteLine(message);
        }
    }
}