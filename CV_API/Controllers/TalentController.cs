using CV_API.Interfaces;
using CV_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace CV_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TalentController(ITalentStore store) : ControllerBase
    {
        [HttpGet("/talent")]
        public ActionResult<IEnumerable<Talent>> GetAll() => Ok(store.GetTalents());

        [HttpGet("/talent/{id}")]
        public ActionResult<Talent> GetById(string id) => Guid.TryParse(id, out var guid) && store.GetTalent(guid) is { } t ? Ok(t) : NotFound();
        [HttpGet("/talent/{id}/documents")]
        public ActionResult<IEnumerable<Document>> GetDocuments(string id) => Guid.TryParse(id, out var guid) ? Ok(store.GetDocuments(guid)) : NotFound();

        [HttpGet("/talent/{id}/documents/{documentId}")]
        public ActionResult<Document> GetDocument(string id, string documentId) =>
            Guid.TryParse(id, out var g1) && Guid.TryParse(documentId, out var g2) && store.GetDocument(g1, g2) is { } d ? Ok(d) : NotFound();

        [HttpGet("/talent/{id}/documents/{documentId}/download")]
        public IActionResult Download(string id, string documentId, [FromServices] IWebHostEnvironment env)
        {
            if (!Guid.TryParse(id, out var talentId) || !Guid.TryParse(documentId, out var docId)) return NotFound();

            var doc = store.GetDocument(talentId, docId);
            if (doc is null || string.IsNullOrWhiteSpace(doc.Content)) return NotFound();

            var webRoot = env.WebRootPath ?? string.Empty;
            var relative = doc.Content.TrimStart('/').Replace('/', Path.DirectorySeparatorChar);

            if (!relative.StartsWith($"documents{Path.DirectorySeparatorChar}", StringComparison.OrdinalIgnoreCase)) return BadRequest("Invalid document Path.");

            var physical = Path.Combine(webRoot, relative);
            if (!System.IO.File.Exists(physical)) return NotFound();

            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(physical, out var contentType)) contentType = "application/octet-stream";

            var downloadName = Path.GetFileName(physical);
            return PhysicalFile(physical, contentType, fileDownloadName: downloadName);
        }
    }
}
