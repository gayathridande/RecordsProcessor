using NUnit.Framework;
using RecordProcessor.Application;
using RecordProcessor.Application.Validators;
using Rhino.Mocks;

namespace RecordProcessor.UnitTests.Application.Validators
{
    [TestFixture]
    public class TestArgumentsValidator
    {
        private ArgumentsValidator _sut;
        private IContentHelper _fileHelper;

        private const string ArgsRequiredErrorMessage = "args are required";

        [SetUp]
        public void Setup()
        {
            _fileHelper = MockRepository.GenerateMock<IContentHelper>();
            _sut = new ArgumentsValidator(_fileHelper);    
        }

        [Test]
        public void ShouldReturnErrorIfArgsAreNull()
        {
            var result = _sut.IsValid(null);
            Assert.That(result.IsValid,Is.False);
            Assert.That(result.ErrorMessage,Is.EqualTo(ArgsRequiredErrorMessage));
        }

        [Test]
        public void ShouldReturnErrorIfArgsAreEmpty()
        {
            var result = _sut.IsValid(new string[]{});
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ErrorMessage, Is.EqualTo(ArgsRequiredErrorMessage));
        }

        [Test]
        public void ShouldRequireFileAndSortArgs()
        {
            var temp1Txt = "temp1.txt";
            var temp2Txt = "temp2.txt";
            var temp3Txt = "temp3.txt";
            var sortArg1 = "-s";
            var sortArg2 = "1";

            _fileHelper.Stub(f => f.Exists(Arg<string>.Is.Anything)).Return(true);

            var result = _sut.IsValid(new[] { temp1Txt, temp2Txt, temp3Txt, sortArg1, sortArg2 });

            Assert.That(result.IsValid, Is.True);
            Assert.That(result.ErrorMessage, Is.Empty);
        }

        [Test]
        public void ShouldRequireSortArgsToBeInRange()
        {
            var temp1Txt = "temp1.txt";
            var temp2Txt = "temp2.txt";
            var temp3Txt = "temp3.txt";
            var sortArg1 = "-s";
            var sortArg2 = "-1";

            _fileHelper.Stub(f => f.Exists(Arg<string>.Is.Anything)).Return(true);

            var result = _sut.IsValid(new[] { temp1Txt, temp2Txt, temp3Txt, sortArg1, sortArg2 });

            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ErrorMessage, Is.StringContaining("sorting_method must be"));
        }

        [Test]
        public void ShouldRequireSortFlag()
        {
            var temp1Txt = "temp1.txt";
            var temp2Txt = "temp2.txt";
            var temp3Txt = "temp3.txt";
            var sortArg1 = "s";
            var sortArg2 = "1";

            _fileHelper.Stub(f => f.Exists(Arg<string>.Is.Anything)).Return(true);

            var result = _sut.IsValid(new[] { temp1Txt, temp2Txt, temp3Txt, sortArg1, sortArg2 });

            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ErrorMessage, Is.StringContaining("sorting_method must be"));
        }

        [Test]
        public void ShouldReturnErrorIfFilesAreNotFound()
        {
            var temp1Txt = "temp1.txt";
            var temp2Txt = "temp2.txt";
            var temp3Txt = "temp3.txt";
            var sortArg1 = "-s";
            var sortArg2 = "1";

            _fileHelper.Stub(f => f.Exists(temp1Txt)).Return(true);
            _fileHelper.Stub(f => f.Exists(temp2Txt)).Return(true);
            _fileHelper.Stub(f => f.Exists(temp3Txt)).Return(false);

            var result = _sut.IsValid(new[] { temp1Txt, temp2Txt, temp3Txt, sortArg1, sortArg2 });

            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ErrorMessage, Is.StringContaining(temp3Txt));
        }
    }
}