using System.Collections.Generic;
using System.Linq;
using RecordProcessor.Application.Domain;
using RecordProcessor.Application.Parsers;
using RecordProcessor.Application.Sorters;

namespace RecordProcessor.Application
{
    public class RecordsBuilder : IBuilder<Record>
    {
        private readonly IContentHelper _contentHelper;
        private readonly IParser<Record> _recordParser;
        private readonly IParser<SortMethod> _sortMethodParser;
        private readonly ISortStrategyFactory _sortStrategyFactory;

        public RecordsBuilder(IContentHelper contentHelper, IParser<Record> recordParser, IParser<SortMethod> sortMethodParser, ISortStrategyFactory sortStrategyFactory)
        {
            _contentHelper = contentHelper;
            _recordParser = recordParser;
            _sortMethodParser = sortMethodParser;
            _sortStrategyFactory = sortStrategyFactory;
        }

        public IEnumerable<Record> Build(string[] pathArgs,string sortingArg)
        {
            var records = new List<Record>();
            var sortingMethod = _sortMethodParser.Parse(sortingArg);
            
            foreach (var content in pathArgs.Select(path => _contentHelper.ReadLines(path)))
            {
                records.AddRange(content.Select(line => _recordParser.Parse(line)));
            }

            var result = _sortStrategyFactory.Get(sortingMethod).Execute(records);
            return result;
        }
    }
}
