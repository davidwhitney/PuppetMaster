using System;
using Nancy;
using Nancy.IO;

namespace PuppetMaster.Domain
{
    public class Registration
    {
        public Guid RegistrationId { get; private set; }
        
        public Url Url { get; set; }
        public string Method { get; set; }
        public RequestHeaders Headers { get; set; }

        public Registration()
        {
            RegistrationId = Guid.NewGuid();
        }

        public static Registration NotRegistered = new Registration();
    }
}