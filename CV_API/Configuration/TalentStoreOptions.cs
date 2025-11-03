using CV_API.Models;

namespace CV_API.Configuration
{
    public sealed class TalentStoreOptions
    {
        public Talent? Talent { get; init; } = new();
        public List<Document> Documents { get; init; } = new();
    }
}
