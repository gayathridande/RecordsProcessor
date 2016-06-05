using NUnit.Framework;
using RecordProcessor.Console;

namespace RecordProcessor.AcceptanceTests.Console
{
    [TestFixture]
    public class TestProgram
    {
        [Test]
        public static void ShouldInitializeContainerAndFail()
        {
            var result = Program.Main(new string[]{});
            Assert.That(result, Is.EqualTo(Program.Error));
        }

        [Test]
        public static void ShouldInitializeContainerAndFailValidation()
        {

            var path = PathHelperForTests.CommaDelimitedFilePath;
            var result = Program.Main(new[] { path });
            Assert.That(result, Is.EqualTo(Program.Error));
        }
    }
}