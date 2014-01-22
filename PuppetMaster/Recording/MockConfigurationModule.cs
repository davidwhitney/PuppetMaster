using System;
using Nancy;
using PuppetMaster.Recording.Storage;

namespace PuppetMaster.Recording
{
    public class MockConfigurationModule : NancyModule
    {
        Guid _registrationId = Guid.Empty;

        public MockConfigurationModule(ICallStore callStore)
        {
            Before += CaptureRegistrationId;

            Get["/_mocks/{registrationId}"] = x => Response.AsJson(callStore.LoadRegistration(_registrationId, Guid.Empty));

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