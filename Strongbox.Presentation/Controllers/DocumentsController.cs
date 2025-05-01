using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Strongbox.Application.DTOs;
using Strongbox.Application.Interfaces;

namespace Strongbox.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController(IDocumentService documentService) : ControllerBase
    {
        [HttpGet("get")]
        public async Task<ActionResult> GetDocument([FromQuery] Guid documentId, [FromQuery] Guid userId)
        {
            var result = await documentService.GetDocumentAsync(documentId, userId);
            if (result == null) return NotFound($"Document with Id {documentId} not found");

            return Ok(result);
        }

        [HttpPut("update")]
        public async Task<ActionResult> UpdateDocument([FromBody] DocumentDto document)
        {
            var result = await documentService.UpdateDocumentAsync(document);
            if (!result) return NotFound("Error in Document update");

            return Ok(result);
        }

        [HttpGet("all")]
        public async Task<ActionResult> GetDocuments([FromQuery] Guid userId)
        {
            var result = await documentService.GetDocumentsAsync(userId);
            if (result == null) return NotFound($"Documents for User Id {userId} not found");

            return Ok(result);
        }

        [HttpGet("attributes")]
        public async Task<ActionResult> GetDocumentsAttributes([FromQuery] Guid userId)
        {
            var result = await documentService.GetDocumentsAttributesAsync(userId);
            if (result == null) return NotFound($"Documents for User Id {userId} not found");

            return Ok(result);
        }
    }
}
