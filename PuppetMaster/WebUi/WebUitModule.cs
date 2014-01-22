using Nancy;

namespace PuppetMaster.WebUi
{
    public class WebUitModule : NancyModule
    {
        public WebUitModule() : base("/_ui")
        {
            Get["/"] = x =>
            {
                return 200;
            };
        }
    }
}