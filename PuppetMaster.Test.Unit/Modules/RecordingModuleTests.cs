﻿using Nancy;
using Nancy.Testing;
using NUnit.Framework;
using PuppetMaster.RequestModeDetection;

namespace PuppetMaster.Test.Unit.Modules
{
    [TestFixture]
    public class RecordingModuleTests : BrowserTest
    {
        [Test]
        public void RequestRoot_RecordModeSpecified_RootedToRecordingModule()
        {
            var result = Browser.Get("/", with => with.Header(PuppetMasterHeaders.ModeHeader, PuppetMasterMode.Record.ToString()));

            Assert.That(result.Headers[PuppetMasterHeaders.RecordingHeader], Is.EqualTo("true"));
        }

        [Test]
        public void RequestRecordingImplicitly_ReturnsAKeyForUserToAssignResponse()
        {
            var result = Browser.Get("/anything", with => with.Header(PuppetMasterHeaders.ModeHeader, PuppetMasterMode.Record.ToString()));

            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(result.Body.AsString(), Is.StringMatching("{\"RegistrationId\":\".+\"}"));
        }

        [Test]
        public void RequestRecordingExplicitly_ReturnsAKeyForUserToAssignResponse()
        {
            var result = Browser.Post("/_mocks", with =>
            {
                with.Body("");
            });

            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(result.Body.AsString(), Is.StringMatching("{\"RegistrationId\":\".+\"}"));
        }
    }
}