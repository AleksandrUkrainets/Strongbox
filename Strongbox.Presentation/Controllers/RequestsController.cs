using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Strongbox.Application.DTOs;
using Strongbox.Application.Interfaces;

namespace Strongbox.Presentation.Controllers
{
    public class RequestsController(IAccessRequestService svc) : ApiBaseController
    {
        [HttpPost("submit")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Create([FromBody] AccessRequestDto dto)
        {
            if (!TryGetCurrentUserId(out var userId)) return Unauthorized();
            var id = await svc.CreateAccessRequestAsync(userId, dto);
            if (id == null) return BadRequest("Cannot create request.");

            return Ok(id);
        }


        [HttpGet("my")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> My()
        {
            if (!TryGetCurrentUserId(out var userId)) return Unauthorized();
            var list = await svc.GetMyRequestsAsync(userId);

            return Ok(list);
        }


        [HttpGet("pending")]
        [Authorize(Roles = "Approver, Admin")]
        public async Task<IActionResult> Pending() => Ok(await svc.GetPendingRequestsAsync());
    }
}
