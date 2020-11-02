using Raven.Client.Documents;

namespace ReadModelRepository.RavenDb.abstraction
{
    public interface IRavenConnectionWrapper
    {
        IDocumentStore Store { get; }
    }
}
