using NUnit.Framework;
using RecordProcessor.Application.Sorters;

namespace RecordProcessor.UnitTests.Application.Sorters
{
    [TestFixture]
    public class TestSortStratedyFactory
    {
        private ISortStrategyFactory _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new SortStrategyFactory();
        }

        [Test]
        public void ShouldReturnFemalesFirstSort()
        {
            var result = _sut.Get(SortMethod.FemalesFirst);
            Assert.That(result,Is.TypeOf<FemalesFirstSortStrategy>());
        }

        [Test]
        public void ShouldReturnBirthDateSort()
        {
            var result = _sut.Get(SortMethod.Birthdate);
            Assert.That(result, Is.TypeOf<BirthDateSortStrategy>());
        }

        [Test]
        public void ShouldReturnLastNameSort()
        {
            var result = _sut.Get(SortMethod.LastName);
            Assert.That(result, Is.TypeOf<LastNameSortStrategy>());
        }

        [Test]
        public void ShouldReturnDefaultSort()
        {
            var result = _sut.Get(SortMethod.None);
            Assert.That(result, Is.TypeOf<DoNotSortStrategy>());
        }

        [Test]
        public void ShouldGetFemaleFirstSortByName()
        {
            var result = _sut.Get("gender");
            Assert.That(result, Is.TypeOf<FemalesFirstSortStrategy>());
        }

        [Test]
        public void ShouldGetBirthDateSortByName()
        {
            var result = _sut.Get("birthdate");
            Assert.That(result, Is.TypeOf<BirthDateSortStrategy>());
        }

        [Test]
        public void ShouldGetLastNameSortByName()
        {
            var result = _sut.Get("name");
            Assert.That(result, Is.TypeOf<LastNameSortStrategy>());
        }

        [Test]
        public void ShouldGetDefaultSortByName()
        {
            Assert.That(_sut.Get("notfound"), Is.TypeOf<DoNotSortStrategy>());
            Assert.That(_sut.Get(""), Is.TypeOf<DoNotSortStrategy>());
        }

        [Test]
        public void ShouldTrimAndLowerWhenGettingSortByName()
        {
            var result = _sut.Get(" GENDER ");
            Assert.That(result, Is.TypeOf<FemalesFirstSortStrategy>());
        }
    }
}