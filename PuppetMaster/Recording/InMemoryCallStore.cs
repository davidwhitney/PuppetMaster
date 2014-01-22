using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Nancy;
using PuppetMaster.Domain;

namespace PuppetMaster.Recording
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
            apiKey = apiKey ?? Guid.Empty;
            var registration = new Registration();

            if (!_registrations.ContainsKey(apiKey.Value))
            {
                _registrations.TryAdd(apiKey.Value, new Dictionary<Guid, Registration>());
            }

            _registrations[apiKey.Value].Add(registration.RegistrationId, registration);

            return registration.RegistrationId;
        }

        public Registration LoadRegistration(Guid requestToken, Guid apiKey)
        {
            if (!_registrations.ContainsKey(apiKey))
            {
                throw new NoRegistrationsForApiKeyException();
            }

            var registrationsPerApiKey = _registrations[apiKey];
            return !registrationsPerApiKey.ContainsKey(requestToken)
                ? Registration.NotRegistered
                : registrationsPerApiKey[requestToken];
        }

        public RegistrationSummaryList ListRegistrations(Guid apiKey)
        {
            var registrations = _registrations[apiKey];

            return new RegistrationSummaryList(apiKey,
                registrations.Select(registration => new RegistrationSummary {RegistrationId = registration.Key}));
        }
    }
}
