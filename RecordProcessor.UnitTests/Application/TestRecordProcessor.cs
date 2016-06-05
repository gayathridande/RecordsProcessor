using System.Collections.Generic;
using NUnit.Framework;
using RecordProcessor.Application;
using RecordProcessor.Application.Domain;
using RecordProcessor.Application.Validators;
using Rhino.Mocks;

namespace RecordProcessor.UnitTests.Application
{
    [TestFixture]
    public class TestRecordProcessor
    {
        private IRecordProcessor _sut;
        private IPrinter _printer;
        private IValidator<string[]> _validator;
        private IBuilder<Record> _builder;

        [SetUp]
        public void Setup()
        {
            _builder = MockRepository.GenerateMock<IBuilder<Record>>();
            _printer = MockRepository.GenerateMock<IPrinter>();
            _validator = MockRepository.GenerateMock<IValidator<string[]>>();
            _sut = new RecordProcessor.Application.RecordProcessor(_builder,_validator,_printer);
        }

        [Test]
        public void ShouldReturnFailureIfValidationFails()
        {
            var args = new string[]{};
            var validationResult = new ValidationResult{IsValid = false, ErrorMessage = "error"};
            
            _validator.Stub(v => v.IsValid(args)).Return(validationResult);

            var result = _sut.Run(args);

            Assert.That(result.Success,Is.False);
            _printer.AssertWasCalled(p => p.Print(validationResult.ErrorMessage));   
        }

        [Test]
        public void ShouldBuildRecords()
        {
            var args = new[] {"1","2","3","4","5"};
            var validationResult = new ValidationResult { IsValid = true};
            var record1 = new Record{FirstName = "first1",LastName = "last1"};
            var record2 = new Record{FirstName = "first2",LastName = "last2"};
            var expectedRecords = new List<Record>{record1, record2};

            _validator.Stub(v => v.IsValid(args)).Return(validationResult);
            _builder.Stub(b => b.Build(Arg<string[]>.Matches(s => s[0] == args[0] && s[1] == args[1] && s[2] == args[2]), Arg<string>.Is.Equal(args[4]))).Return(expectedRecords);

            var result = _sut.Run(args);

            Assert.That(result.Success,Is.True);
            Assert.That(result.Records,Is.SameAs(expectedRecords));
            Assert.That(result.DisplayMessage,Is.StringContaining(record1.FirstName));
            Assert.That(result.DisplayMessage,Is.StringContaining(record1.LastName));
            Assert.That(result.DisplayMessage,Is.StringContaining(record2.FirstName));
            Assert.That(result.DisplayMessage,Is.StringContaining(record2.LastName));

        }


        [Test]
        public void ShouldPrintRecords()
        {
            var args = new[] { "1", "2", "3", "4", "5" };
            var validationResult = new ValidationResult { IsValid = true };
            var record1 = new Record { FirstName = "first1", LastName = "last1" };
            var expectedRecords = new List<Record> { record1 };

            _validator.Stub(v => v.IsValid(args)).Return(validationResult);
            _builder.Stub(b => b.Build(Arg<string[]>.Is.Anything, Arg<string>.Is.Anything)).Return(expectedRecords);

            var result = _sut.Run(args);

            _printer.AssertWasCalled(p => p.Print(result.DisplayMessage));
        }
    }
}