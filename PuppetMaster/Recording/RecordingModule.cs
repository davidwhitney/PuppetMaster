﻿using Nancy;
using PuppetMaster.Domain;
using PuppetMaster.RequestModeDetection;

namespace PuppetMaster.Recording
{
    public class RecordingModule : NancyModule
    {
        public RecordingModule()
        {
            After += ctx => ctx.Response.Headers.Add(PuppetMasterHeaders.RecordingHeader, "true");
            
            Get["/(.*)", ctx => ctx.ModeIs(PuppetMasterMode.Record)] = x =>
            {
                return Response.AsJson(new RecordingRequestedResponse());
            };
        }

    }
}