using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using RecordProcessor.Application;
using RecordProcessor.Application.Domain;
using RecordProcessor.Application.Parsers;
using RecordProcessor.Application.Sorters;
using Rhino.Mocks;

namespace RecordProcessor.UnitTests.Application
{
    [TestFixture]
    public class TestRecordsBuilder
    {
        private IContentHelper _contentHelper;
        private IParser<Record> _recordParser;
        private IParser<SortMethod> _sortMethodParser;
        private RecordsBuilder _sut;
        private ISortStrategyFactory _sortingFactory;

        [SetUp]
        public void Setup()
        {
            _recordParser = MockRepository.GenerateMock<IParser<Record>>();
            _sortMethodParser = MockRepository.GenerateMock<IParser<SortMethod>>();
            _contentHelper = MockRepository.GenerateMock<IContentHelper>();
            _sortingFactory = MockRepository.GenerateMock<ISortStrategyFactory>();
            _sut = new RecordsBuilder(_contentHelper, _recordParser, _sortMethodParser,_sortingFactory);
        }

        [Test]
        public void ShouldReadFileAndParse()
        {
            var path1 = "path1";
            var line1 = "line1";
            var expectedRecord = new Record();
            var paths = new[] {path1};
            var linesOfContent = new List<string>{line1};
            var sortArg = "1";
            var sortEnum = SortMethod.FemalesFirst;
            var mockSort = MockRepository.GenerateMock<ISortStrategy>();
            var expectedResult = new List<Record>();

            _contentHelper.Stub(h => h.ReadLines(path1)).Return(linesOfContent);
            _recordParser.Stub(p => p.Parse(line1)).Return(expectedRecord);
            _sortMethodParser.Stub(p => p.Parse(sortArg)).Return(sortEnum);
            _sortingFactory.Stub(f => f.Get(sortEnum)).Return(mockSort);
            mockSort.Stub(s => s.Execute(Arg<IEnumerable<Record>>.Matches(r => r.Count() == linesOfContent.Count && r.First() == expectedRecord))).Return(expectedResult);

            var result = _sut.Build(paths, sortArg);

            Assert.That(result,Is.SameAs(expectedResult));
        }

        [Test]
        public void ShouldReadMultipleFilesAndParse()
        {
            var path1 = "path1";
            var path2 = "path2";
            var line1 = "line1";
            var line2 = "line2";
            var expectedRecord1 = new Record{FirstName = "Ryan"};
            var expectedRecord2 = new Record{FirstName = "Charlie"};
            var linesOfContent1 = new List<string> { line1 };
            var linesOfContent2 = new List<string> { line2 };
            var expectedRecordTotal = linesOfContent1.Count + linesOfContent2.Count;
            var paths = new[] { path1, path2 };
            var sortArg = "1";
            var sortEnum = SortMethod.FemalesFirst;
            var mockSort = MockRepository.GenerateMock<ISortStrategy>();
            var expectedResult = new List<Record>();

            _contentHelper.Stub(h => h.ReadLines(path1)).Return(linesOfContent1);
            _contentHelper.Stub(h => h.ReadLines(path2)).Return(linesOfContent2);
            _recordParser.Stub(p => p.Parse(line1)).Return(expectedRecord1);
            _recordParser.Stub(p => p.Parse(line2)).Return(expectedRecord2);
            _sortMethodParser.Stub(p => p.Parse(sortArg)).Return(sortEnum);
            _sortingFactory.Stub(f => f.Get(sortEnum)).Return(mockSort);
            mockSort.Stub(s => s.Execute(Arg<IEnumerable<Record>>.Matches(r => r.Count() == expectedRecordTotal && r.First() == expectedRecord1 && r.Last() == expectedRecord2))).Return(expectedResult);

            var result = _sut.Build(paths, sortArg);

            Assert.That(result, Is.SameAs(expectedResult));
        }
    }
}