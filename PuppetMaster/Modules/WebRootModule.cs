using Nancy;

namespace PuppetMaster.Modules
{
    public class WebRootModule : NancyModule
    {
        public WebRootModule()
        {
            Get["/"] = x => 200;
        }
    }
}