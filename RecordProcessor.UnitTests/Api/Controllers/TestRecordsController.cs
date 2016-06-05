using System.Collections.Generic;
using System.Web.Http.Results;
using NUnit.Framework;
using RecordProcessor.Api.Controllers;
using RecordProcessor.Application.Domain;
using RecordProcessor.Application.Repositories;
using RecordProcessor.Application.Sorters;
using Rhino.Mocks;

namespace RecordProcessor.UnitTests.Api.Controllers
{
    [TestFixture]
    public class TestRecordsController
    {
        private RecordsController _sut;
        private ISortStrategyFactory _sortFactory;
        private IRecordRepository _recordRepository;

        [SetUp]
        public void Setup()
        {
            _recordRepository = MockRepository.GenerateMock<IRecordRepository>();
            _sortFactory = MockRepository.GenerateMock<ISortStrategyFactory>();
            _sut = new RecordsController(_sortFactory,_recordRepository);
        }

        [Test]
        public void ShouldGetAllRecords()
        {
            var records = new List<Record>();
            _recordRepository.Stub(r => r.GetAll()).Return(records);
            var result = _sut.Get() as OkNegotiatedContentResult<IEnumerable<Record>>;
            Assert.That(result,Is.Not.Null);
            Assert.That(result.Content, Is.SameAs(records));
        }

        [Test]
        public void ShouldGetSortedRecords()
        {
            const string sortingName = "sort";
            var records = new List<Record>();
            var sortedRecords = new List<Record>();
            var mockStrategy = MockRepository.GenerateMock<ISortStrategy>();
            _recordRepository.Stub(r => r.GetAll()).Return(records);
            _sortFactory.Stub(f => f.Get(sortingName)).Return(mockStrategy);
            mockStrategy.Stub(f => f.Execute(records)).Return(sortedRecords);
            
            var result = _sut.Get(sortingName) as OkNegotiatedContentResult<IEnumerable<Record>>;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Content, Is.Not.SameAs(records));
            Assert.That(result.Content, Is.SameAs(sortedRecords));
        }

        [Test]
        public void ShouldAddRecord()
        {
            var record = new Record();
            var result = _sut.Post(record);
            Assert.IsInstanceOf<OkResult>(result);
            _recordRepository.AssertWasCalled(r => r.Add(record));
        }
    }
}