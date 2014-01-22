using System.Configuration;
using System.Linq;
using Nancy;

namespace PuppetMaster.RequestModeDetection
{
    public static class NancyRequestExtensions
    {
        public static bool InMode(this Request req, PuppetMasterMode mode)
        {
            var recordPort = ConfigurationManager.AppSettings["RecordPort"] ?? "";
            var replayPort = ConfigurationManager.AppSettings["ReplayPort"] ?? "";

            if(req.Url.HostName.Contains(mode.ToString().ToLower()))
            {
                return true;
            }

            var port = req.Url.Port.GetValueOrDefault(80);
            if (port.ToString() == recordPort && mode == PuppetMasterMode.Record)
            {
                return true;
            }

            if (port.ToString() == replayPort && mode == PuppetMasterMode.Replay)
            {
                return true;
            }

            var header = req.Headers.SingleOrDefault(x => x.Key == PuppetMasterHeaders.ModeHeader);

            if (header.Value == null && mode == PuppetMasterMode.Web)
            {
                return true;
            }

            if (header.Value != null && header.Value.First() == mode.ToString())
            {
                return true;
            }

            return false;
        }
    }
}