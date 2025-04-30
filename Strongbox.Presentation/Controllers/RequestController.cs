using Microsoft.AspNetCore.Mvc;
using Strongbox.Application.DTOs;
using Strongbox.Application.Interfaces;

namespace Strongbox.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController(IAccessRequestService requestService) : ControllerBase
    {
        [HttpPost("submit")]
        public async Task<ActionResult> CreateAccessRequest([FromBody] AccessRequestDto accessRequestDto)
        {
            var result = await requestService.CreateAccessRequestAsync(accessRequestDto);

            if (result == null) return BadRequest("Error in Access Request creation");

            return Ok(result);
        }

        [HttpGet("check")]
        public async Task<ActionResult> GetAccessRequest([FromQuery] Guid requestId, [FromQuery] Guid userId)
        {
            var result = await requestService.GetAccessRequestAsync(requestId, userId);
            if (result == null) return NotFound($"Access request with Id {requestId} not found");

            return Ok(result);
        }

        [HttpGet("all")]
        public async Task<ActionResult> GetUserAccessRequests([FromQuery] Guid userId)
        {
            var result = await requestService.GetUserAccessRequestsAsync(userId);
            if (result == null) return NotFound($"Access request for User Id {userId} not found");

            return Ok(result);
        }

        [HttpGet("pending")]
        public async Task<ActionResult> GetPendingAccessRequests([FromQuery] Guid approverId)
        {
            var result = await requestService.GetPendingAccessRequestsAsync(approverId);
            if (result == null) return NotFound($"Access request for Approver Id {approverId} not found");

            return Ok(result);
        }
    }
}
