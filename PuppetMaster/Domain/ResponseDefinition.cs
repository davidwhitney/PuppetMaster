using System.Collections.Generic;

namespace PuppetMaster.Domain
{
    public class ResponseDefinition
    {
        public int HttpStatusCode { get; set; }
        public string HttpStatusMessage { get; set; }
        public string HttpBody { get; set; }
        public Dictionary<string, string> ResponseHeaders { get; set; }

        public ResponseDefinition()
        {
            ResponseHeaders = new Dictionary<string, string>();
        }
    }
}