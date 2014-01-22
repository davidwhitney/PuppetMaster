using System;

namespace PuppetMaster.Domain
{
    public class RecordingRequestedResponse
    {
        public Guid RegistrationId { get; set; }

        public RecordingRequestedResponse()
        {
            RegistrationId = Guid.NewGuid();
        }
    }
}