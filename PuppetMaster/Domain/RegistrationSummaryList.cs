using System;
using System.Collections.Generic;

namespace PuppetMaster.Domain
{
    public class RegistrationSummaryList : List<RegistrationSummary>
    {
        public Guid ApiKey { get; set; }

        public RegistrationSummaryList(Guid apiKey) : this(apiKey, new List<RegistrationSummary>())
        {
        }

        public RegistrationSummaryList(Guid apiKey, IEnumerable<RegistrationSummary> items)
            : base(items)
        {
            ApiKey = apiKey;
        }
    }
}