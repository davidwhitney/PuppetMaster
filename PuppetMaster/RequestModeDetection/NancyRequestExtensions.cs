using System.Linq;
using Nancy;

namespace PuppetMaster.RequestModeDetection
{
    public static class NancyRequestExtensions
    {
        public static bool InMode(this Request req, PuppetMasterMode mode)
        {
            if(req.Url.HostName.Contains(mode.ToString().ToLower()))
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