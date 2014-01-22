using Nancy;

namespace PuppetMaster.Modules
{
    public class RecordingModule : NancyModule
    {
        public RecordingModule()
        {
            Get["/(.*)", ctx => ctx.Request.Url.HostName.Contains("record")] = x =>
            {
                return "record";
            };
        }
    }
}