﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Strongbox.Application.DTOs;
using Strongbox.Application.Interfaces;

namespace Strongbox.Presentation.Controllers
{
    [AllowAnonymous]
    public class AuthController(IAuthService svc) : ApiBaseController
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var result = await svc.RegisterAsync(dto);
            if (result == null) return BadRequest("Username already taken.");
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var result = await svc.LoginAsync(dto);
            if (result == null) return Unauthorized();
            return Ok(result);
        }
    }
}
