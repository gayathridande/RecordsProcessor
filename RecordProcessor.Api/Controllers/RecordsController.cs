using System.Web.Http;
using RecordProcessor.Application.Domain;
using RecordProcessor.Application.Repositories;
using RecordProcessor.Application.Sorters;

namespace RecordProcessor.Api.Controllers
{
    public class RecordsController : ApiController
    {
        private readonly ISortStrategyFactory _sortStrategyFactory;
        private readonly IRecordRepository _recordRepository;

        public RecordsController(ISortStrategyFactory sortStrategyFactory, IRecordRepository recordRepository)
        {
            _sortStrategyFactory = sortStrategyFactory;
            _recordRepository = recordRepository;
        }

        // GET: records
        public IHttpActionResult Get()
        {
            var records = _recordRepository.GetAll();
            return Ok(records);
        }

        // GET: records/sort-strategy
        public IHttpActionResult Get(string id)
        {
            var records = _recordRepository.GetAll();
            var sortedRecords = _sortStrategyFactory.Get(id).Execute(records);
            return Ok(sortedRecords);
        }

        // POST: records
        public IHttpActionResult Post(Record record)
        {
            _recordRepository.Add(record);
            return Ok();
        }
    }
}
