using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using RecordProcessor.Application.Domain;
using RecordProcessor.Application.Repositories;

namespace RecordProcessor.UnitTests.Application.Repositories
{
    [TestFixture]
    public class TestInMemoryRecordRepository
    {
        private IList<Record> _records;
        private IRecordRepository _sut;

        [SetUp]
        public void Setup()
        {
            _records = new List<Record>();
            _sut = new InMemoryRecordRepository(_records);
        }

        [Test]
        public void ShouldGetAll()
        {
            Assert.That(_sut.GetAll(),Is.SameAs(_records));
        }

        [Test]
        public void ShouldAddRecord()
        {
            var count = _records.Count;
            var record = new Record();
            _sut.Add(record);
            Assert.That(_records.Count,Is.EqualTo(count + 1));
            Assert.That(_records.Last(),Is.SameAs(record));
        }
    }
}