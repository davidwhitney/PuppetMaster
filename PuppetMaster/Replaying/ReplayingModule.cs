using System;
using System.IO;
using System.Text;
using Nancy;
using PuppetMaster.Recording.Storage;
using PuppetMaster.RequestModeDetection;

namespace PuppetMaster.Replaying
{
    public class ReplayingModule : NancyModule
    {
        public ReplayingModule(ICallStore callStore)
        {
            Get["/(.*)", when => when.ModeIs(PuppetMasterMode.Replay)] = x =>
            {
                var registration = callStore.LoadRegistration(Request.Url, Guid.Empty);

                return new Response
                {
                    StatusCode = (HttpStatusCode) registration.Response.HttpStatusCode,
                    ReasonPhrase = registration.Response.HttpStatusMessage,
                    Contents = stream =>
                    {
                        using (var writer = new StreamWriter(stream))
                        {
                            writer.Write(registration.Response.HttpBody);
                            writer.Flush();
                        }
                    }
                };
            };
        }
    }
}
