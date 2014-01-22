using System;
using Nancy;
using Nancy.ModelBinding;
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
                return Response.AsJson(callStore.ListRegistrations(Guid.Empty));
            };

            Post["/_mocks"] = x =>
            {
                var registration = this.Bind<Registration>();
                var registrationId = callStore.RegisterCall(registration, Guid.Empty);
                return Response.AsJson(new RecordingRequestedResponse { RegistrationId = registrationId });
            };

            Get["/_mocks/{registrationId}"] = x =>
            {
                CaptureRegistrationId(Context);
                return Response.AsJson(callStore.LoadRegistration(_registrationId, Guid.Empty));
            };

            Post["/_mocks/{registrationId}/response"] = x =>
            {
                CaptureRegistrationId(Context);
                var response = this.Bind<ResponseDefinition>();
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