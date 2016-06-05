using System;
using System.Collections.Generic;
using System.Linq;
using RecordProcessor.Application.Sorters;

namespace RecordProcessor.Application.Validators
{
    public class ArgumentsValidator : IValidator<string[]>
    {

        private readonly int _minSortMethod;
        private readonly int _maxSortMethod;
        private readonly IContentHelper _helper;

        public ArgumentsValidator(IContentHelper helper)
        {
            _helper = helper;
            _minSortMethod = Enum.GetValues(typeof(SortMethod)).Cast<int>().Min();
            _maxSortMethod = Enum.GetValues(typeof(SortMethod)).Cast<int>().Max();
        }

        public ValidationResult IsValid(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                return new ValidationResult{IsValid = false, ErrorMessage = "args are required"};
            }
            if (args.Length != 5)
            {
                return new ValidationResult{IsValid = false, ErrorMessage = "args are required in the form \"file_path1 file_path2 file_path3 -s sorting_method\""};
            }
            if (args[3] != "-s" || !IsValidSortingMethod(args[4]))
            {
                var errorMessage = string.Format("the last two args must be \"-s sorting_method\" where sorting_method must be from {0} to {1}",_minSortMethod,_maxSortMethod);
                return new ValidationResult { IsValid = false, ErrorMessage = errorMessage };
            }

            return ValidateFilesExist(args.Take(3));
        }

        private bool IsValidSortingMethod(string sort)
        {
            int parsedValue;
            var validParse = Int32.TryParse(sort, out parsedValue);
            return validParse 
                && _minSortMethod <= parsedValue 
                && parsedValue <= _maxSortMethod;
        }

        private ValidationResult ValidateFilesExist(IEnumerable<string> args)
        {
            var errors = new List<string>();
            foreach (var path in args)
            {
                if (!_helper.Exists(path))
                {
                    errors.Add(string.Format("{0} is not a valid file path",path));
                }
            }
            return new ValidationResult {IsValid = !errors.Any(), ErrorMessage = string.Join("\n", errors)};
        }
    }
}
