using System;
using RecordProcessor.Application.Sorters;

namespace RecordProcessor.Application.Parsers
{
    public class SortMethodParser : IParser<SortMethod>
    {
        public SortMethod Parse(string data)
        {
            var sort = Int32.Parse(data);
            return (SortMethod) sort;
        }
    }
}
