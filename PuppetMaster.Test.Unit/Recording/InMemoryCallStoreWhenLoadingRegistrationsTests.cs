using System;
using System.Collections.Generic;
using Nancy;
using NUnit.Framework;
using PuppetMaster.Domain;
using PuppetMaster.Recording;

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
        public void LoadRegistration_InvalidRequestToken_Throws()
        {
            var response = _store.LoadRegistration(_requestToken, _apiKey);

            Assert.That(response, Is.EqualTo(Registration.NotRegistered));
        }

        [Test]
        public void LoadRegistration_ValidRequestToken_ReturnsRegistration()
        {
            var response = _store.LoadRegistration(_requestToken, _apiKey);

            Assert.That(response, Is.Not.Null);
        }
    }
}