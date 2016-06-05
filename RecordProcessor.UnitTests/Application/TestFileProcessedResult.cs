using NUnit.Framework;
using RecordProcessor.Application;

namespace RecordProcessor.UnitTests.Application
{
    [TestFixture]
    public class TestFileProcessedResult
    {
        private RecordsProcessedResult _sut;
        private string _successMessage;
        private string _errorMessage;

        [SetUp]
        public void Setup()
        {
            _successMessage = "a success";
            _errorMessage = "a failure";
            _sut = new RecordsProcessedResult{SuccessMessage = _successMessage, ErrorMessage = _errorMessage};
        }

        [Test]
        public void ShouldDisplaySuccessMessage()
        {
            _sut.Success = true;
            var result = _sut.DisplayMessage;
            Assert.That(result,Is.EqualTo(_successMessage));
        }

        [Test]
        public void ShouldDisplayErrorMessage()
        {
            _sut.Success = false;
            var result = _sut.DisplayMessage;
            Assert.That(result, Is.EqualTo(_errorMessage));
        }
    }
}