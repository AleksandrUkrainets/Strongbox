using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Strongbox.Application.DTOs;
using Strongbox.Application.Interfaces;

namespace Strongbox.Presentation.Controllers
{
    public class UsersController(IUserService svc) : ApiBaseController
    {
        [HttpGet("all")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var list = await svc.GetAllUsersAsync();

            return Ok(list);
        }

        [HttpPut("role")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangeRole([FromBody] ChangeUserRoleDto dto)
        {
            var ok = await svc.ChangeUserRoleAsync(dto);
            if (!ok) return NotFound();

            return NoContent();
        }

        [HttpPut("password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
        {
            var success = await svc.ChangePasswordAsync(dto);
            if (!success) return BadRequest("Bad User Name or Old Password");

            return NoContent();
        }
    }
}
