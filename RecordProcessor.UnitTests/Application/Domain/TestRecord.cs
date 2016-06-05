using System;
using NUnit.Framework;
using RecordProcessor.Application.Domain;

namespace RecordProcessor.UnitTests.Application.Domain
{
    [TestFixture]
    public class TestRecord
    {
        private Record _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new Record{FirstName = "Ryan", LastName = "Ferretti", Gender = "Male", FavoriteColor = "Green", BirthDate = new DateTime(1981,9,8)};    
        }

        [Test]
        public void ShouldToString()
        {
            var result = _sut.ToString();
            var expected = string.Format("Ryan Ferretti (Male) likes the color Green and was born on {0}",_sut.BirthDate);
            Assert.That(result, Is.EqualTo(expected));
        }
    }
}