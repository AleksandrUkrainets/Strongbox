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
        public async Task<IActionResult> GetAttributes()
            => Ok(await svc.GetDocumentsAttributesAsync());


        [HttpGet("get")]
        public async Task<IActionResult> GetDocument([FromQuery] Guid documentId)
        {
            var document = await svc.GetDocumentAsync(documentId);
            if (document == null) return NotFound();

            return Ok(document);
        }


        [HttpGet("approved")]
        public async Task<IActionResult> GetApproved()
            => Ok(await svc.GetApprovedDocumentsAsync());
    }
}
