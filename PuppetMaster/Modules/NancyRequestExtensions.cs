﻿using System.Linq;
using Nancy;

namespace PuppetMaster.Modules
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

            if (header.Value != null && header.Value.First() == PuppetMasterMode.Record.ToString())
            {
                return true;
            }

            return false;
        }
    }
}