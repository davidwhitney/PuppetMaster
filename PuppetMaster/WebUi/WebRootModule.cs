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
                var vm = new WebrootViewModel
                {
                    RecordPort = cfg.AppSettings.AppSetting<int>("ProxyPort"),
                    ReplyPort = cfg.AppSettings.AppSetting<int>("RecordPort")
                };

                return View["index.cshtml", vm];
            };
        }
    }

    public class WebrootViewModel
    {
        public int RecordPort { get; set; }
        public int ReplyPort { get; set; }
    }
}