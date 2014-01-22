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
        private readonly ConcurrentDictionary<Guid, Dictionary<Guid, Registration>> _registrations;

        public InMemoryCallStore()
        {
            _registrations = new ConcurrentDictionary<Guid, Dictionary<Guid, Registration>>();
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
                _registrations.TryAdd(apiKey.Value, new Dictionary<Guid, Registration>());
            }
            
            _registrations[apiKey.Value].Add(registration.RegistrationId, registration);

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
                throw new NoRegistrationsForApiKeyException();
            }

            var uriMatch = calledUri.ToString().Replace(":8080", "");
            var registration = _registrations[apiKey].FirstOrDefault(x => x.Value.Request.Url.ToString().Contains(uriMatch));

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
                registrations.Select(registration => new RegistrationSummary {RegistrationId = registration.Key}));
        }
    }
}