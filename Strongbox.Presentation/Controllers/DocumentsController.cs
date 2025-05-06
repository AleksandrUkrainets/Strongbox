using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Strongbox.Application.DTOs;
using Strongbox.Application.Interfaces;

namespace Strongbox.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DocumentsController(IDocumentService svc) : ControllerBase
    {
        [HttpGet("attributes")]
        public async Task<IActionResult> GetAttributes([FromQuery] Guid userId)
            => Ok(await svc.GetDocumentsAttributesAsync(userId));


        [HttpGet("get")]
        public async Task<IActionResult> GetDocument([FromQuery] Guid documentId, [FromQuery] Guid userId)
        {
            try
            {
                var document = await svc.GetDocumentAsync(documentId, userId);
                if (document == null) return NotFound();
                return Ok(document);
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
        }


        [HttpGet("approved")]
        public async Task<IActionResult> GetApproved([FromQuery] Guid userId)
            => Ok(await svc.GetApprovedDocumentsAsync(userId));
    }
}
