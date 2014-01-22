using System;
using Nancy;
using PuppetMaster.Domain;
using PuppetMaster.Recording.Storage;
using PuppetMaster.RequestModeDetection;

namespace PuppetMaster.Recording
{
    public class RecordingModule : NancyModule
    {
        public RecordingModule(ICallStore callStore)
        {
            After += ctx => ctx.Response.Headers.Add(PuppetMasterHeaders.RecordingHeader, "true");
            
            Get["/(.*)", when => when.ModeIs(PuppetMasterMode.Record)] = x =>
            {
                var registrationId = callStore.RegisterCall(Request, Guid.Empty);
                return Response.AsJson(new RecordingRequestedResponse{RegistrationId = registrationId});
            };
        }
    }
}