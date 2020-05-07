﻿using Chess.Core.Middlewares.Models;
using Chess.Users.Models;
using Chess.Users.Models.UserModels;
using Chess.Users.Services.EntityServices.Interfaces;
using Chess.Users.Services.Interfaces;
using Chess.Users.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Chess.Users.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    [ProducesResponseType(typeof(ErrorModel), 400)]
    [ProducesResponseType(typeof(ErrorModel), 401)]
    [ProducesResponseType(typeof(ErrorModel), 404)]
    public class AuthController : Controller
    {
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(ITokenService tokenService,
            IUserService userService,
            ILogger<AuthController> logger)
        {
            _tokenService = tokenService;
            _userService = userService;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
                var userModel = await _userService.GetByEmailAsync(loginModel.Email);
                if (userModel == default)
                    return NotFound(new ErrorModel
                    {
                        Message = $"No user with email:{loginModel.Email} found.",
                        StatusCode = 404
                    });
                
                if (!PasswordHasher.VerifyPassword(loginModel.Password, userModel.Password))
                    return StatusCode(401, new ErrorModel
                    {
                        Message = "Password incorrect",
                        StatusCode = 401
                    });
                
                var token = await _tokenService.GenerateJwtAsync(userModel);

                return Ok(token);
        }
    }
}