using CV_API.Interfaces;
using CV_API.Models;
using Microsoft.Extensions.Options;
using System.Collections.Immutable;

namespace CV_API.Services
{
    public sealed class InMemoryTalentStore : ITalentStore
    {
        private readonly Talent _talent;
        private readonly ImmutableArray<Document> _documents;
        private readonly ImmutableDictionary<Guid, Document> _documentById;

        public InMemoryTalentStore(IOptions<TalentStoreOptions> options)
        {
            var config = options.Value ?? throw new ArgumentNullException(nameof(options));

            if (config.Talent is null)
            {
                throw new InvalidOperationException("Talent configuration is missing.");
            }
            if (config.Talent.Id == Guid.Empty)
            {
                throw new InvalidOperationException("Talent Id must be set.");
            }

            _talent = config.Talent;
            _documents = (config.Documents ?? new()).ToImmutableArray();
            _documentById = _documents.ToImmutableDictionary(doc => doc.Id);
        }

        public IReadOnlyList<Talent> GetTalents() => new[] { _talent };
        public Talent? GetTalent(Guid id) => id == _talent.Id ? _talent : null;

        public IReadOnlyList<Document> GetDocuments(Guid talentId) => talentId == _talent.Id ? _documents : ImmutableArray<Document>.Empty;

        public Document? GetDocument(Guid talentId, Guid documentId) => talentId == _talent.Id && _documentById.TryGetValue(documentId, out var document) ? document : null;
    }
}
