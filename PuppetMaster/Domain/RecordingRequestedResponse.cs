using System;

namespace PuppetMaster.Domain
{
    public class RecordingRequestedResponse
    {
        public Guid RequestKey { get; set; }

        public RecordingRequestedResponse()
        {
            RequestKey = Guid.NewGuid();
        }
    }
}