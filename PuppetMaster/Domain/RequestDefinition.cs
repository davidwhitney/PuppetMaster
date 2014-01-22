using System;
using Nancy;

namespace PuppetMaster.Domain
{
    public class RequestDefinition
    {
        public Uri Url { get; set; }
        public string Method { get; set; }
        public RequestHeaders Headers { get; set; }
    }
}