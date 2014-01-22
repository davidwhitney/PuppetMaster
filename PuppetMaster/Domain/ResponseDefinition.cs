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
            HttpStatusCode = 200;
            HttpStatusMessage = "OK";
            HttpBody = "This method is not yet configured, POST a Registration to /_mocks/{registrationId} to configure.";
            ResponseHeaders = new Dictionary<string, string>();
        }
    }
}