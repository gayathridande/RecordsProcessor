using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using RecordProcessor.Application.Domain;
using RecordProcessor.Application.Sorters;

namespace RecordProcessor.UnitTests.Application.Sorters
{
    [TestFixture]
    public class TestBirthDateSortStrategy
    {
        private ISortStrategy _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new BirthDateSortStrategy();
        }

        [Test]
        public void ShouldSortCorrectly()
        {
            var records = new List<Record>{new Record{LastName = "B", BirthDate = new DateTime(2010,1,1)},
                                           new Record{LastName = "X", BirthDate = new DateTime(2001,1,1)},
                                           new Record{LastName = "N", BirthDate = new DateTime(2002,1,1)},
                                           new Record{LastName = "Z", BirthDate = new DateTime(2013,1,1)},
                                           new Record{LastName = "M", BirthDate = new DateTime(2004,1,1)},
                                           new Record{LastName = "C", BirthDate = new DateTime(2015,1,1)}};

            var result = _sut.Execute(records).ToArray();

            Assert.That(result[0].LastName,Is.EqualTo("X"));
            Assert.That(result[1].LastName,Is.EqualTo("N"));
            Assert.That(result[2].LastName,Is.EqualTo("M"));
            Assert.That(result[3].LastName,Is.EqualTo("B"));
            Assert.That(result[4].LastName,Is.EqualTo("Z"));
            Assert.That(result[5].LastName,Is.EqualTo("C"));
        }
    }
}