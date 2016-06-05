using System.Collections.Generic;
using RecordProcessor.Application.Domain;

namespace RecordProcessor.Application.Repositories
{
    public class InMemoryRecordRepository : IRecordRepository
    {
        private readonly IList<Record> _records;

        public InMemoryRecordRepository(IList<Record> records)
        {
            _records = records;
        }

        public IEnumerable<Record> GetAll()
        {
            return _records;
        }

        public void Add(Record record)
        {
            _records.Add(record);
        }
    }
}
