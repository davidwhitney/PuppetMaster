﻿using System;
using Nancy;
using Nancy.ModelBinding;
using Newtonsoft.Json;
using PuppetMaster.Domain;
using PuppetMaster.Recording.Storage;

namespace PuppetMaster.Recording
{
    public class MockConfigurationModule : NancyModule
    {
        Guid _registrationId = Guid.Empty;

        public MockConfigurationModule(ICallStore callStore)
        {
            Get["/_mocks"] = x =>
            {
                return Negotiate.WithModel(callStore.ListRegistrations(Guid.Empty));
            };

            Post["/_mocks"] = x =>
            {
                var registration = this.Bind<Registration>();
                var registrationId = callStore.RegisterCall(registration, Guid.Empty);
                return Response.AsJson(new RecordingRequestedResponse { RegistrationId = registrationId }, HttpStatusCode.Created);
            };

            Get["/_mocks/{registrationId}"] = x =>
            {
                CaptureRegistrationId(Context);
                return Negotiate.WithModel(callStore.LoadRegistration(_registrationId, Guid.Empty));
                //return Response.AsJson(callStore.LoadRegistration(_registrationId, Guid.Empty));
            };

            Post["/_mocks/{registrationId}/response"] = x =>
            {
                CaptureRegistrationId(Context);
                
                var response = this.Bind<ResponseDefinition>();
                if (Context.Request.Form["json"] != null)
                {
                    response = JsonConvert.DeserializeObject<ResponseDefinition>(Context.Request.Form["json"].Value);
                }

                callStore.ConfigureResponse(_registrationId, response);
                return HttpStatusCode.Accepted;
            };
        }

        private void CaptureRegistrationId(NancyContext ctx)
        {
            Guid.TryParse(ctx.Parameters.registrationId.Value, out _registrationId);
        }
    }
}