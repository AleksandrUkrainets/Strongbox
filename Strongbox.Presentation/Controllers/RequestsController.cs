using Microsoft.AspNetCore.Mvc;
using Strongbox.Application.DTOs;
using Strongbox.Application.Interfaces;

namespace Strongbox.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RequestsController(IAccessRequestService svc) : ControllerBase
    {
        [HttpPost("submit")]
        public async Task<IActionResult> Create([FromBody] AccessRequestDto dto)
        {
            var id = await svc.CreateAccessRequestAsync(dto);
            if (id == null) return BadRequest("Cannot create request.");
            return Ok(id);
        }


        [HttpGet("my")]
        public async Task<IActionResult> My([FromQuery] Guid userId)
            => Ok(await svc.GetMyRequestsAsync(userId));


        [HttpGet("pending")]
        public async Task<IActionResult> Pending([FromQuery] Guid approverId)
        {
            try
            {
                return Ok(await svc.GetPendingRequestsAsync(approverId));
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
        }
    }
}
