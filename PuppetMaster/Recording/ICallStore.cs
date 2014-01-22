using System;
using Nancy;
using PuppetMaster.Domain;

namespace PuppetMaster.Recording
{
    public interface ICallStore
    {
        Guid RegisterCall(Request request, Guid? apiKey = null);
        Registration LoadRegistration(Guid registrationId, Guid apiKey);
        RegistrationSummaryList ListRegistrations(Guid apiKey);
    }
}