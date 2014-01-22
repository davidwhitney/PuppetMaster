﻿using System;
using Nancy;
using Nancy.Testing;
using Newtonsoft.Json;
using NUnit.Framework;
using PuppetMaster.Domain;
using PuppetMaster.RequestModeDetection;

namespace PuppetMaster.Test.Unit.Modules
{
    [TestFixture]
    public class RecordingModuleTests : BrowserTest
    {
        [Test]
        [Ignore("TODO: Support this scenario")]
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
            var response = new Registration
            {
                Request = new RequestDefinition
                {
                    Url = new Uri("http://localhost/i-am-mocked")
                },
                Response = new ResponseDefinition
                {
                    HttpStatusCode = 200,
                    HttpStatusMessage = "Awesomely OK"
                }
            };

            var result = Browser.Post("/_mocks", with => with.Body(JsonConvert.SerializeObject(response)));

            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(result.Body.AsString(), Is.StringMatching("{\"RegistrationId\":\".+\"}"));
        }

        [Test]
        public void RequestRecordingExplicitly_ThenRequested_MockedResponseReturned()
        {
            var mockObject = JsonConvert.SerializeObject(new Registration
            {
                Request = new RequestDefinition
                {
                    Url = new Uri("http://localhost/i-am-mocked"),
                    Method = "GET"
                },
                Response = new ResponseDefinition
                {
                    HttpStatusCode = 200,
                    HttpStatusMessage = "Awesomely OK",
                    HttpBody = "Mock here!"
                }
            });

            Browser.Post("/_mocks", with =>
            {
                with.Body(mockObject);
                with.Header("Content-type", "application/json");
            });

            var result = Browser.Get("/i-am-mocked", with => with.Header(PuppetMasterHeaders.ModeHeader, PuppetMasterMode.Replay.ToString()));

            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(result.Body.AsString(), Is.StringContaining("Mock here!"));
        }
    }
}