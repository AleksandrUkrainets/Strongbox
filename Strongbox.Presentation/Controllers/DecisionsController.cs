using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Strongbox.Application.DTOs;
using Strongbox.Application.Interfaces;

namespace Strongbox.Presentation.Controllers
{
    [Authorize(Roles = "Approver, Admin")]
    public class DecisionsController(IDecisionService svc) : ApiBaseController
    {
        [HttpPost("submit")]
        public async Task<IActionResult> Create([FromBody] DecisionDto dto)
        {
            if (!TryGetCurrentUserId(out var userId)) return Unauthorized();
            var id = await svc.CreateDecisionAsync(userId, dto);
            if (id == null) return BadRequest("Cannot create decision.");

            return Ok(id);
        }


        [HttpGet("all")]
        public async Task<IActionResult> All()
            => Ok(await svc.GetDecisionsAsync());


        [HttpGet(("get"))]
        public async Task<IActionResult> Get([FromQuery] Guid decisionId)
        {
            var decision = await svc.GetDecisionAsync(decisionId);
            if (decision == null) return NotFound();

            return Ok(decision);
        }


        [HttpPut("update/{decisionId}")]
        public async Task<IActionResult> Update(Guid decisionId, [FromBody] DecisionDto dto)
        {
            var ok = await svc.UpdateDecisionAsync(decisionId, dto);
            if (!ok) return BadRequest("Cannot update decision.");
            return NoContent();
        }
    }
}
