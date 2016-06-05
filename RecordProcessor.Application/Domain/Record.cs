using System;

namespace RecordProcessor.Application.Domain
{
    public class Record
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string FavoriteColor { get; set; }
        public DateTime BirthDate { get; set; }
        public override string ToString()
        {
            return string.Format("{0} {1} ({2}) likes the color {3} and was born on {4}", FirstName, LastName, Gender, FavoriteColor, BirthDate);
        }
    }
}
