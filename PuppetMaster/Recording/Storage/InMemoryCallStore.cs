using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Nancy;
using PuppetMaster.Domain;

namespace PuppetMaster.Recording.Storage
{
    public class InMemoryCallStore : ICallStore
    {
        private readonly ConcurrentDictionary<Guid, ConcurrentDictionary<Guid, Registration>> _registrations;
        private readonly ConcurrentDictionary<Guid, Guid> _registrationsToApiKey;

        public InMemoryCallStore()
        {
            _registrations = new ConcurrentDictionary<Guid, ConcurrentDictionary<Guid, Registration>>();
            _registrationsToApiKey = new ConcurrentDictionary<Guid, Guid>();
        }

        public Guid RegisterCall(Request request, Guid? apiKey = null)
        {
            if (string.IsNullOrEmpty(request.Url.HostName))
            {
                request.Url.HostName = "localhost";
            }

            var registration = new Registration
            {
                Request = new RequestDefinition
                {
                    Url = request.Url,
                    Headers = request.Headers,
                    Method = request.Method,
                }
            };

            return RegisterCall(registration, apiKey);
        }

        public Guid RegisterCall(Registration registration, Guid? apiKey = null)
        {
            apiKey = apiKey ?? Guid.Empty;
            if (!_registrations.ContainsKey(apiKey.Value))
            {
                _registrations.TryAdd(apiKey.Value, new ConcurrentDictionary<Guid, Registration>());
            }
            
            _registrations[apiKey.Value].TryAdd(registration.RegistrationId, registration);
            _registrationsToApiKey.TryAdd(registration.RegistrationId, apiKey.Value);

            return registration.RegistrationId;
        }

        public Registration LoadRegistration(Guid registrationId, Guid apiKey)
        {
            if (!_registrations.ContainsKey(apiKey))
            {
                throw new NoRegistrationsForApiKeyException();
            }

            var registrationsPerApiKey = _registrations[apiKey];
            return !registrationsPerApiKey.ContainsKey(registrationId)
                ? Registration.NotRegistered
                : registrationsPerApiKey[registrationId];
        }

        public Registration LoadRegistration(Url calledUri, Guid apiKey)
        {
            if (!_registrations.ContainsKey(apiKey))
            {
                return new Registration();
            }

            calledUri.HostName = string.IsNullOrWhiteSpace( calledUri.HostName ) ? "localhost" : calledUri.HostName;
            var uriMatch = calledUri.ToString().Replace(":8080", "").Replace(":85", "");
            var registration = _registrations[apiKey].FirstOrDefault(x => x.Value.Request.Url.ToString().ToString().Replace(":8080", "").Replace(":85", "").Contains(uriMatch));

            return registration.Value;
        }

        public RegistrationSummaryList ListRegistrations(Guid apiKey)
        {
            if (!_registrations.ContainsKey(apiKey))
            {
                return new RegistrationSummaryList(apiKey, new List<RegistrationSummary>());
            }

            var registrations = _registrations[apiKey];

            return new RegistrationSummaryList(apiKey,
                registrations.Select(
                    registration =>
                        new RegistrationSummary
                        {
                            RegistrationId = registration.Key,
                            Uri = registration.Value.Request.Url.ToString(),
                            Method = registration.Value.Request.Method
                        }));
        }

        public void ConfigureResponse(Guid registrationId, ResponseDefinition response)
        {
            var apiKey = _registrationsToApiKey[registrationId];
            var apiKeyRegistrations = _registrations[apiKey];
            var call = apiKeyRegistrations[registrationId];

            call.Response = response;
        }
    }
}