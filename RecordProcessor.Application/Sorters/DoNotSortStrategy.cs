using System.Collections.Generic;
using RecordProcessor.Application.Domain;

namespace RecordProcessor.Application.Sorters
{
    public class DoNotSortStrategy : ISortStrategy
    {
        public IEnumerable<Record> Execute(IEnumerable<Record> records)
        {
            return records;
        }
    }

}
