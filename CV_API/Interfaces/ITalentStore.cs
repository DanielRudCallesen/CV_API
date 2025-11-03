using CV_API.Models;

namespace CV_API.Interfaces
{
    public interface ITalentStore
    {
        IReadOnlyList<Talent> GetTalents();
        Talent? GetTalent(Guid id);
        IReadOnlyList<Document> GetDocuments(Guid talentId);
        Document? GetDocument(Guid talentId, Guid documentId);
    }
}
