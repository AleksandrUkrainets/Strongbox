using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Strongbox.Application.Interfaces;

namespace Strongbox.Presentation.Controllers
{
    [Authorize]
    public class DocumentsController(IDocumentService svc) : ApiBaseController
    {
        [HttpGet("attributes")]
        public async Task<IActionResult> GetAttributes()
        {
            if (!TryGetCurrentUser(out var userId, out var role)) return Unauthorized();

            var result = await svc.GetDocumentsAttributesAsync(userId, role);

            return Ok(result);
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetDocument([FromQuery] Guid documentId)
        {
            if (!TryGetCurrentUser(out var userId, out var role)) return Unauthorized();

            var document = await svc.GetDocumentAsync(userId, role, documentId);
            if (document == null) return NotFound();

            return Ok(document);
        }


        [HttpGet("approved")]
        public async Task<IActionResult> GetApproved()
        {
            if (!TryGetCurrentUser(out var userId, out var role)) return Unauthorized();

            var list = await svc.GetApprovedDocumentsAsync(userId, role);

            return Ok(list);
        }
    }
}
