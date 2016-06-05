using System.Collections.Generic;
using System.IO;

namespace RecordProcessor.Application
{
    public class FileHelper : IContentHelper
    {
        public bool Exists(string path)
        {
            return File.Exists(path);
        }

        public IEnumerable<string> ReadLines(string path)
        {
            return File.ReadLines(path);
        }
    }
}
