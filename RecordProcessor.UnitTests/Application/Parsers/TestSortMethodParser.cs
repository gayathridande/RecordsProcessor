using NUnit.Framework;
using RecordProcessor.Application.Parsers;
using RecordProcessor.Application.Sorters;

namespace RecordProcessor.UnitTests.Application.Parsers
{
    [TestFixture]
    public class TestSortMethodParser
    {
        private IParser<SortMethod> _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new SortMethodParser();
        }

        [Test]
        public void ShouldReturnSortMethod()
        {
            Assert.That(_sut.Parse("0"),Is.EqualTo(SortMethod.None));
            Assert.That(_sut.Parse("1"),Is.EqualTo(SortMethod.FemalesFirst));
            Assert.That(_sut.Parse("2"),Is.EqualTo(SortMethod.Birthdate));
            Assert.That(_sut.Parse("3"),Is.EqualTo(SortMethod.LastName));
        }
    }
}