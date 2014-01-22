using System;
using System.Linq;
using Nancy;
using NUnit.Framework;
using PuppetMaster.Domain;
using PuppetMaster.Recording.Storage;

namespace PuppetMaster.Test.Unit.Recording
{
    [TestFixture]
    public class InMemoryCallStoreWhenRegisteringCallsTests
    {
        private InMemoryCallStore _store;
        private Request _simpleRequest;

        [SetUp]
        public void SetUp()
        {
            _store = new InMemoryCallStore();
            _simpleRequest = new Request("GET", "/", "http");
        }

        [Test]
        public void RegisterCall_WithoutApiKey_ReturnsRegistrationTokenForDefaultSession()
        {
            var response = _store.RegisterCall(_simpleRequest);

            Assert.That(response, Is.TypeOf<Guid>());
            Assert.That(response, Is.Not.EqualTo(Guid.Empty));
        }

        [Test]
        public void RegisterCall_ForApiKey_GeneratesRegistrationToken()
        {
            var apiKey = Guid.NewGuid();

            var response = _store.RegisterCall(_simpleRequest, apiKey);

            Assert.That(response, Is.TypeOf<Guid>());
            Assert.That(response, Is.Not.EqualTo(Guid.Empty));
        }

        [Test]
        public void RegisterCall_ExplicitCallWithDto_GeneratesRegistrationToken()
        {
            var apiKey = Guid.NewGuid();

            var response = _store.RegisterCall(new Registration(), apiKey);

            Assert.That(response, Is.TypeOf<Guid>());
            Assert.That(response, Is.Not.EqualTo(Guid.Empty));
        }

        [Test]
        public void ListRegistrations_ForApiKey_ReturnsRegistrationIndex()
        {
            var apiKey = Guid.NewGuid();
            var registrationKey = _store.RegisterCall(_simpleRequest, apiKey);

            var registrations = _store.ListRegistrations(apiKey);

            Assert.That(registrations.ApiKey, Is.EqualTo(apiKey));
            Assert.That(registrations.First().RegistrationId, Is.EqualTo(registrationKey));
        }
    }
}
