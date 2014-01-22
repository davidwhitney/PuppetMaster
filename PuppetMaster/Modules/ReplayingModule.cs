﻿using Nancy;

namespace PuppetMaster.Modules
{
    public class ReplayingModule : NancyModule
    {
        public ReplayingModule()
        {
            Get["/(.*)", ctx => ctx.Request.Url.HostName.Contains("replay")] = x =>
            {
                return "replay";
            };
        }
    }
}
