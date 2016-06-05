using System;
using System.Linq;
using RecordProcessor.Application.Domain;

namespace RecordProcessor.Application.Parsers
{
    public class RecordParser : IParser<Record>
    {
        private readonly string[] _delimiters;

        public RecordParser(string[] delimiters)
        {
            _delimiters = delimiters;
        }

        public Record Parse(string recordData)
        {
            // TODO: assumption, all data in files is valid and delimiter is unique
            var delimiter = _delimiters.First(recordData.Contains).ToCharArray();
            var values = recordData.Split(delimiter, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).ToArray();
            return new Record
            {
                LastName = values[0],
                FirstName = values[1],
                Gender = values[2],
                FavoriteColor = values[3],
                BirthDate = DateTime.Parse(values[4])
            };
        }
    }
}
