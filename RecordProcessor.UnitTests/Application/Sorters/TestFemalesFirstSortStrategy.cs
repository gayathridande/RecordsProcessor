using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using RecordProcessor.Application.Domain;
using RecordProcessor.Application.Sorters;

namespace RecordProcessor.UnitTests.Application.Sorters
{
    [TestFixture]
    public class TestFemalesFirstSortStrategy
    {
        private ISortStrategy _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new FemalesFirstSortStrategy();
        }

        [Test]
        public void ShouldSortCorrectly()
        {
            var records = new List<Record>{new Record{LastName = "B", Gender = "Female"},
                                           new Record{LastName = "X", Gender = "Female"},
                                           new Record{LastName = "N", Gender = "Female"},
                                           new Record{LastName = "Z", Gender = "Male"},
                                           new Record{LastName = "M", Gender = "Male"},
                                           new Record{LastName = "C", Gender = "Male"}};

            var result = _sut.Execute(records).ToArray();

            Assert.That(result[0].LastName,Is.EqualTo("B"));
            Assert.That(result[1].LastName,Is.EqualTo("N"));
            Assert.That(result[2].LastName,Is.EqualTo("X"));
            Assert.That(result[3].LastName,Is.EqualTo("C"));
            Assert.That(result[4].LastName,Is.EqualTo("M"));
            Assert.That(result[5].LastName,Is.EqualTo("Z"));
        }
    }
}