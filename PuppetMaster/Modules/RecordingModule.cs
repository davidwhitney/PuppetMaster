﻿using Nancy;
using PuppetMaster.Domain;
using PuppetMaster.RequestModeDetection;

namespace PuppetMaster.Modules
{
    public class RecordingModule : NancyModule
    {
        public RecordingModule()
        {
            After += ctx => ctx.Response.Headers.Add(PuppetMasterHeaders.RecordingHeader, "true");

            Get["/", ctx => ctx.Request.InMode(PuppetMasterMode.Record)] = x =>
            {
                return "record";
            };

            Get["/(.*)", ctx => ctx.Request.InMode(PuppetMasterMode.Record)] = x =>
            {
                return Response.AsJson(new RecordingRequestedResponse());
            };
        }

    }
}