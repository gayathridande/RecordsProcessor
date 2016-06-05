using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;
using Autofac;
using Autofac.Integration.WebApi;
using NUnit.Framework;
using RecordProcessor.Api;
using RecordProcessor.Api.Controllers;
using RecordProcessor.Api.IoC;
using RecordProcessor.Application.Domain;

namespace RecordProcessor.AcceptanceTests.Api
{
    [TestFixture]
    class TestApi
    {
        private RecordsController _sut;

        [SetUp]
        public void Setup()
        {
            var container = BuildContainer();
            SetupPostInitDependencies(container);
            _sut = container.Resolve<RecordsController>();
        }

        [Test]
        public void ShouldGetAllRecords()
        {
            var results = _sut.Get() as OkNegotiatedContentResult<IEnumerable<Record>>;
            Assert.That(results, Is.Not.Null);
            Assert.That(results.Content.Count(), Is.EqualTo(30));
            Assert.That(results.Content.First().FirstName, Is.EqualTo("Ryan"));
            Assert.That(results.Content.First().LastName, Is.EqualTo("Ferretti"));
        }

        [Test]
        public void ShouldGetAllRecordsSorted()
        {
            var results = _sut.Get("gender") as OkNegotiatedContentResult<IEnumerable<Record>>;
            Assert.That(results, Is.Not.Null);
            Assert.That(results.Content.Count(), Is.EqualTo(30));
            Assert.That(results.Content.First().FirstName, Is.EqualTo("Amy"));
            Assert.That(results.Content.First().LastName, Is.EqualTo("Andrews"));
        }
        
        [Test]
        public void ShouldAddRecords()
        {
            var record = new Record{FirstName = "Frank", LastName = "Zappa", BirthDate = new DateTime(1960,1,1), Gender = "Male", FavoriteColor = "Black"};
            var records = _sut.Get() as OkNegotiatedContentResult<IEnumerable<Record>>;
            var initialCount = records.Content.Count();
            
            var addResult = _sut.Post(record);
            Assert.IsInstanceOf<OkResult>(addResult);

            var results = _sut.Get() as OkNegotiatedContentResult<IEnumerable<Record>>;
            Assert.That(results.Content.Count(), Is.EqualTo(initialCount + 1));
            Assert.That(results.Content.Last().FirstName, Is.EqualTo(record.FirstName));
            Assert.That(results.Content.Last().LastName, Is.EqualTo(record.LastName));
        }

        protected void SetupPostInitDependencies(IContainer container)
        {
            var newBuilder = new ContainerBuilder();
            newBuilder.RegisterModule(new PostApplyApiModule(container, WebApiApplication.RecordPaths));
            newBuilder.Update(container);
        }

        private IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();
            var apiModule = new ApiModule();
            builder.RegisterModule(apiModule);
            builder.RegisterApiControllers(apiModule.GetType().Assembly);
            return builder.Build();
        }
    }
}
