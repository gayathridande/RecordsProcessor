using System.Collections.Generic;

namespace RecordProcessor.Application
{
    public interface IBuilder<T>
    {
        IEnumerable<T> Build(string[] pathArgs,string sortingArg);
    }
}