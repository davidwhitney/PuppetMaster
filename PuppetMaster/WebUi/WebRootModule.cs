using Nancy;
using PuppetMaster.RequestModeDetection;

namespace PuppetMaster.WebUi
{
    public class WebRootModule : NancyModule
    {
        public WebRootModule()
        {
            Before += ctx => !ctx.Request.InMode(PuppetMasterMode.Web) ? (Response) 404 : null;

            Get["/"] = x =>
            {
                return 200;
            };
        }
    }
}