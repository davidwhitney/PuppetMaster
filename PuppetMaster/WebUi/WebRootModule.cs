using System.Configuration.Abstractions;
using Nancy;
using PuppetMaster.RequestModeDetection;

namespace PuppetMaster.WebUi
{
    public class WebRootModule : NancyModule
    {
        public WebRootModule(IConfigurationManager cfg)
        {
            Before += when => when.ModeIsNot(PuppetMasterMode.Web) ? (Response) 404 : null;

            Get["/"] = x =>
            {
                var vm = cfg.AppSettings.Map<WebrootViewModel>();
                return View["index.cshtml", vm];
            };
        }
    }

    public class WebrootViewModel
    {
        public int RecordPort { get; set; }
        public int ProxyPort { get; set; }
    }
}