using System;
using Nancy;
using PuppetMaster.Domain;

namespace PuppetMaster.Recording.Storage
{
    public interface ICallStore
    {
        Guid RegisterCall(Request request, Guid? apiKey = null);
        Guid RegisterCall(Registration registration, Guid? apiKey = null);
        Registration LoadRegistration(Guid registrationId, Guid apiKey);
        Registration LoadRegistration(Url calledUri, Guid apiKey);
        RegistrationSummaryList ListRegistrations(Guid apiKey);
        void ConfigureResponse(Guid registrationId, ResponseDefinition response);
    }
}