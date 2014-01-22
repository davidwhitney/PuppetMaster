using System;
using System.Collections.Generic;
using Nancy;
using NUnit.Framework;
using PuppetMaster.Domain;
using PuppetMaster.Recording;
using PuppetMaster.Recording.Storage;

namespace PuppetMaster.Test.Unit.Recording
{
    [TestFixture]
    public class InMemoryCallStoreWhenLoadingRegistrationsTests
    {
        private InMemoryCallStore _store;
        private Request _simpleRequest;
        private Guid _apiKey;
        private Guid _requestToken;

        [SetUp]
        public void SetUp()
        {
            _store = new InMemoryCallStore();
            _simpleRequest = new Request("GET", "/", "http");
            _apiKey = Guid.NewGuid();
            _requestToken = _store.RegisterCall(_simpleRequest, _apiKey);
        }

        [Test]
        public void LoadRegistration_InvalidApiKey_Throws()
        {
            Assert.Throws<NoRegistrationsForApiKeyException>(() => _store.LoadRegistration(_requestToken, Guid.NewGuid()));
        }

        [Test]
        public void LoadRegistration_UnrecognisedRequestToken_ReturnsUnregisteredRequest()
        {
            var response = _store.LoadRegistration(Guid.NewGuid(), _apiKey);

            Assert.That(response, Is.EqualTo(Registration.NotRegistered));
        }
        
        [Test]
        public void LoadRegistration_ValidRequestToken_ReturnsRegistration()
        {
            var response = _store.LoadRegistration(_requestToken, _apiKey);

            Assert.That(response, Is.Not.Null);
        }

        [Test]
        public void ListRegistrations_NoRegistrationsRegistered_ReturnsEmptyList()
        {
            _store = new InMemoryCallStore();

            var response = _store.ListRegistrations(_apiKey);

            Assert.That(response, Is.Not.Null);
            Assert.That(response, Is.Empty);
        }
    }
}