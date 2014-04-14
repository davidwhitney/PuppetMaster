using System;
using System.IO;
using Nancy;
using PuppetMaster.Recording.Storage;
using PuppetMaster.RequestModeDetection;

namespace PuppetMaster.Proxying
{
    public class ProxyingModule : NancyModule
    {
        public ProxyingModule(ICallStore callStore)
        {
            Get["/(.*)", when => when.ModeIs(PuppetMasterMode.Proxy)] = x => ReturnRecording(callStore);
            Put["/(.*)", when => when.ModeIs(PuppetMasterMode.Proxy)] = x => ReturnRecording(callStore);
            Post["/(.*)", when => when.ModeIs(PuppetMasterMode.Proxy)] = x => ReturnRecording(callStore);
            Delete["/(.*)", when => when.ModeIs(PuppetMasterMode.Proxy)] = x => ReturnRecording(callStore);
        }

        private dynamic ReturnRecording(ICallStore callStore)
        {
            var registration = callStore.LoadRegistration(Request.Url, Guid.Empty);

            return new Response
            {
                StatusCode = (HttpStatusCode) registration.Response.HttpStatusCode,
                ReasonPhrase = registration.Response.HttpStatusMessage,
                Contents = stream =>
                {
                    var writer = new StreamWriter(stream);
                    writer.Write(registration.Response.HttpBody);
                    writer.Flush();
                }
            };
        }
    }
}
