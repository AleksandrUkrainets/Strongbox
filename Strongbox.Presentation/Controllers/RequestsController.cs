using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Strongbox.Application.DTOs;
using Strongbox.Application.Interfaces;

namespace Strongbox.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
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
        public async Task<IActionResult> My() => Ok(await svc.GetMyRequestsAsync());


        [HttpGet("pending")]
        [Authorize(Roles = "Approver, Admin")]
        public async Task<IActionResult> Pending() => Ok(await svc.GetPendingRequestsAsync());
    }
}
