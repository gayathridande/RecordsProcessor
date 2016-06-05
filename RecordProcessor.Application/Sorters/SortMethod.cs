namespace RecordProcessor.Application.Sorters
{
    public enum SortMethod
    {
        None = 0,
        FemalesFirst = 1,
        Birthdate = 2,
        LastName = 3
    }

    public class SortStrategyFactory : ISortStrategyFactory
    {
        public ISortStrategy Get(SortMethod sortMethod)
        {
            switch (sortMethod)
            {
                case SortMethod.FemalesFirst:
                    return new FemalesFirstSortStrategy();
                case SortMethod.Birthdate:
                    return new BirthDateSortStrategy();
                case SortMethod.LastName:
                    return new LastNameSortStrategy();
                default:
                    return new DoNotSortStrategy();
            }
        }

        public ISortStrategy Get(string sortMethodName)
        {
            var sort = sortMethodName.Trim().ToLower();
            switch (sort)
            {
                case "gender":
                    return Get(SortMethod.FemalesFirst);
                case "birthdate":
                    return Get(SortMethod.Birthdate);
                case "name":
                    return Get(SortMethod.LastName);
                default:
                    return Get(SortMethod.None);
            }
        }
    }
}
