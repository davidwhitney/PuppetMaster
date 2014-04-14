using System;

namespace PuppetMaster.Domain
{
    public class Registration
    {
        public Guid RegistrationId { get; set; }

        public RequestDefinition Request { get; set; }
        public ResponseDefinition Response { get; set; }

        public Registration()
        {
            RegistrationId = Guid.NewGuid();
            Request = new RequestDefinition();
            Response = new ResponseDefinition();
        }

        public static Registration NotRegistered = new Registration();
    }
}