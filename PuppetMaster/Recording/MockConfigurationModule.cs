﻿using System;
using Nancy;

namespace PuppetMaster.Recording
{
    public class MockConfigurationModule : NancyModule
    {
        Guid _registrationId = Guid.Empty;

        public MockConfigurationModule(ICallStore callStore)
        {
            Before += CaptureRegistrationId;


            Get["/_mocks/{registrationId}"] = x =>
            {
                var registration = callStore.LoadRegistration(_registrationId, Guid.Empty);
                return Response.AsJson(registration);
            };

            Post["/_mocks/{registrationId}/response"] = x =>
            {
                return 200;
            };
        }

        private Response CaptureRegistrationId(NancyContext ctx)
        {
            if (!ctx.Parameters.registrationId.HasValue)
            {
                return 400;
            }

            var success = Guid.TryParse(ctx.Parameters.registrationId.Value, out _registrationId);
            if (!success)
            {
                return 400;
            }
            return null;
        }
    }
}