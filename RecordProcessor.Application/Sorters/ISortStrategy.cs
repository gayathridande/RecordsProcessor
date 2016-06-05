using System.Collections.Generic;
using RecordProcessor.Application.Domain;

namespace RecordProcessor.Application.Sorters
{
    public interface ISortStrategy
    {
        IEnumerable<Record> Execute(IEnumerable<Record> records);
    }
}