using Nancy;
using PuppetMaster.RequestModeDetection;

namespace PuppetMaster.Replaying
{
    public class ReplayingModule : NancyModule
    {
        public ReplayingModule()
        {
            Get["/(.*)", when => when.ModeIs(PuppetMasterMode.Replay)] = x =>
            {
                return "replay";
            };
        }
    }
}
