using System;

namespace PuppetMaster.Domain
{
    public class Registration
    {
        public Guid RegistrationId { get; private set; }

        public Registration()
        {
            RegistrationId = Guid.NewGuid();
        }

        public static Registration NotRegistered = new Registration();
    }
}