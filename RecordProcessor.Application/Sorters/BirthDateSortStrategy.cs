using System.Collections.Generic;
using System.Linq;
using RecordProcessor.Application.Domain;

namespace RecordProcessor.Application.Sorters
{
    public class BirthDateSortStrategy : ISortStrategy
    {
        public IEnumerable<Record> Execute(IEnumerable<Record> records)
        {
            return records.OrderBy(r => r.BirthDate);
        }
    }
}