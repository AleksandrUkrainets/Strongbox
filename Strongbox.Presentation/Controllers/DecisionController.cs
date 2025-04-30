using Microsoft.AspNetCore.Mvc;
using Strongbox.Application.DTOs;
using Strongbox.Application.Interfaces;

namespace Strongbox.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DecisionController(IDecisionService decisionService) : ControllerBase
    {
        [HttpPost("submit")]
        public async Task<ActionResult> CreateDecision([FromBody] DecisionDto decision)
        {
            var result = await decisionService.CreateDecisionAsync(decision);

            if (result == null) return BadRequest("Error in Decision creation");

            return Ok(result);
        }

        [HttpGet("get")]
        public async Task<ActionResult> GetDecision([FromQuery] Guid decisionId, [FromQuery] Guid approverId)
        {
            var result = await decisionService.GetDecisionAsync(decisionId, approverId);
            if (result == null) return NotFound($"Decision with Id {decisionId} not found");

            return Ok(result);
        }

        [HttpPut("update")]
        public async Task<ActionResult> UpdateDecision([FromBody] DecisionDto decision)
        {
            var result = await decisionService.UpdateDecisionAsync(decision);
            if (result == null) return NotFound("Error in Decision update");

            return Ok(result);
        }

        [HttpGet("all")]
        public async Task<ActionResult> GetApproverDecisions([FromQuery] Guid approverId)
        {
            var result = await decisionService.GetApproverDecisionsAsync(approverId);
            if (result == null) return NotFound($"Decisions for Approver Id {approverId} not found");

            return Ok(result);
        }
    }
}
