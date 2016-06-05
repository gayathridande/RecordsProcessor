using System.Collections.Generic;

namespace RecordProcessor.Application
{
    public interface IContentHelper
    {
        bool Exists(string path);

        IEnumerable<string> ReadLines(string path);
    }
}
