using System.IO;

namespace RecordProcessor.AcceptanceTests
{
    public class PathHelperForTests
    {
        public static string CommaDelimitedFilePath { get { return Path.Combine(SolutionPath, "records_comma.txt"); } }
        public static string PipeDelimitedFilePath { get { return Path.Combine(SolutionPath, "records_pipe.txt"); } }
        public static string SpaceDelimitedFilePath { get { return Path.Combine(SolutionPath, "records_space.txt"); } }

        private static string SolutionPath
        {
            get { return Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName; }
        }
    }
}
