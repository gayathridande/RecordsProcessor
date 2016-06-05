using System.Collections.Generic;
using RecordProcessor.Application.Domain;

namespace RecordProcessor.Application.Repositories
{
    public interface IRecordRepository
    {
        IEnumerable<Record> GetAll();
        void Add(Record record);
    }
}