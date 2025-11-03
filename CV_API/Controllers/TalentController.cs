using CV_API.Interfaces;
using CV_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CV_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TalentController(ITalentStore store) : ControllerBase
    {
        [HttpGet("/talent")]
        public ActionResult<IEnumerable<Talent>> GetAll() => Ok(store.GetTalents());

        [HttpGet("/talent/{id}")]
        public ActionResult<Talent> GetById(string id) => Guid.TryParse(id, out var guid ) ? Ok(store.GetDocuments(guid)) : NotFound();

        [HttpGet("/talent/{id}/documents")]
        public ActionResult<IEnumerable<Document>> GetDocuments(string id) => Guid.TryParse(id, out var guid) ? Ok(store.GetDocuments(guid)) : NotFound();

        [HttpGet("/talent/{id}/documents/{documentId}")]
        public ActionResult<Document> GetDocument(string id, string documentId) =>
            Guid.TryParse(id, out var g1) && Guid.TryParse(documentId, out var g2) && store.GetDocument(g1, g2) is { } d ? Ok(d) : NotFound();
    }
}
