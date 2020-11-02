using System;
using Raven.Client.Documents;
using ReadModelRepository.RavenDb.abstraction;

namespace ReadModelRepository.RavenDb.implementation
{
    public class RavenDbConnectionWrapper : IRavenConnectionWrapper
    {
        private readonly RavenSettings _options;
        public IDocumentStore Store { get; }

        public RavenDbConnectionWrapper(RavenSettings ravenSettings)
        {
            _options = ravenSettings;
            Store = new DocumentStore()
            {
                Urls = _options.Urls ?? throw new ArgumentNullException(nameof(_options.Urls)),
                Database = _options.Database ?? "RavenDb"
            };
            
            Store.Conventions.SaveEnumsAsIntegers = false;
            Store.Conventions.UseOptimisticConcurrency = true;
            Store.Conventions.MaxNumberOfRequestsPerSession = 2;
            Store.Initialize();
        }
    }
}