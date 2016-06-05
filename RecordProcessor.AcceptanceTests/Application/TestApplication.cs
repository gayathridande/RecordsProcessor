using System;
using Autofac;
using NUnit.Framework;
using RecordProcessor.Application;
using RecordProcessor.Console.IoC;
using Rhino.Mocks;

namespace RecordProcessor.AcceptanceTests.Application
{
    [TestFixture]
    public class TestApplication
    {
        private IPrinter _mockPrinter;
        private IRecordProcessor _sut;

        [SetUp]
        public void Setup()
        {
            var container = BuildContainer();
            _sut = container.Resolve<IRecordProcessor>();
        }

        [Test]
        public void ShouldReturnFailure()
        {
            var result = _sut.Run(new string[] { });
            Assert.That(result.Success, Is.False);
            _mockPrinter.AssertWasCalled(p => p.Print(Arg<string>.Matches(s => s.Contains("args are required"))));
        }

        [Test]
        public void ShouldPrintSortedRecords()
        {
            var firstExpectedName = "Amy Andrews";
            var lastExpectedBirth = new DateTime(1970,2,28);
            var commaFile = PathHelperForTests.CommaDelimitedFilePath;
            var pipeFile = PathHelperForTests.PipeDelimitedFilePath;
            var spaceFile = PathHelperForTests.SpaceDelimitedFilePath;
            _sut.Run(new[] { commaFile, pipeFile, spaceFile, "-s", "1" });
            _mockPrinter.AssertWasCalled(p => p.Print(Arg<string>.Matches(s => s.StartsWith(firstExpectedName) && s.EndsWith(lastExpectedBirth.ToString()))));
        }

        private IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new ConsoleModule());
            OverrideAutoFacRegistrations(builder);
            return builder.Build();
        }

        private void OverrideAutoFacRegistrations(ContainerBuilder builder)
        {
            _mockPrinter = MockRepository.GenerateMock<IPrinter>();
            builder.Register(c => _mockPrinter).As<IPrinter>();
        }
    }
}