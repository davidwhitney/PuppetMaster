using System;

namespace PuppetMaster.Domain
{
    public class RegistrationSummary
    {
        public Guid RegistrationId { get; set; }
        public string Uri { get; set; }
        public string Method { get; set; }
    }
}