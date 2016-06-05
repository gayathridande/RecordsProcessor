using System;
using NUnit.Framework;
using RecordProcessor.Application.Domain;
using RecordProcessor.Application.Parsers;

namespace RecordProcessor.UnitTests.Application.Parsers
{
    [TestFixture]
    public class TestRecordParser
    {
        private IParser<Record> _sut;
        private string[] _delimiters;

        [SetUp]
        public void Setup()
        {
            _delimiters = new[] {"|", ",", " "};
            _sut = new RecordParser(_delimiters);
        }

        [TestCase("|")]
        [TestCase(",")]
        [TestCase(" ")]
        public void ShouldReturnRecord(string delimiter)
        {
            var firstName = "Ryan";
            var lastName = "Ferretti";
            var gender = "Male";
            var color = "Green";
            var birth = new DateTime(1981,9,8);
            var input = BuildRecordInput(delimiter, firstName, lastName, gender, color, birth);

            var result = _sut.Parse(input);

            Assert.That(result.FirstName, Is.EqualTo(firstName));
            Assert.That(result.LastName, Is.EqualTo(lastName));
            Assert.That(result.Gender, Is.EqualTo(gender));
            Assert.That(result.FavoriteColor, Is.EqualTo(color));
            Assert.That(result.BirthDate, Is.EqualTo(birth));
        }

        private string BuildRecordInput(string delimiter, string firstName, string lastName, string gender, string color, DateTime birthDate)
        {
            return string.Format("{1} {0} {2} {0} {3} {0} {4} {0} {5}", delimiter, lastName, firstName, gender, color, birthDate);
        }
    }
}