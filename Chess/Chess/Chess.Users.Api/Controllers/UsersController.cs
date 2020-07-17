using Chess.Core.Api.Controllers;
using Chess.Core.Middlewares.Models;
using Chess.Users.DataAccess.Entities;
using Chess.Users.Models.UserModels;
using Chess.Users.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Chess.UsersService.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : BaseCrudController<IUserService, UserModel, User>
    {
        public UsersController(IUserService service,
            ILogger<UsersController> logger)
            : base(service, logger) { }

        [HttpPost]
        public override async Task<IActionResult> Post(UserModel model)
        {
            if (await _service.DoesUserExistAsync(model.Email))
                return BadRequest(new ErrorModel
                {
                    Message = $"Email already exists.",
                    StatusCode = 400
                });

            await base.Post(model);
            return Ok();
        }

        [HttpPost("change-password/{userId}")]
        public async Task<IActionResult> ChangePassword(Guid userId, [FromBody] ChangePasswordModel model)
        {
            await _service.ChangePasswordAsync(userId, model);
            await _service.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("details/{userId}")]
        public async Task<IActionResult> Details(Guid userId)
        {
            var userDetails = await _service.GetUserDetailsAsync(userId);

            if (userDetails == default)
                return NotFound(new ErrorModel
                {
                    Message = $"Model with Id {userId} not found",
                    StatusCode = 400
                });

            return Ok(userDetails);
        }

        [HttpPatch("details/{userId}")]
        public async Task<IActionResult> Details(Guid userId, [FromBody] UserDetailsModel model)
        {
            await _service.UpdateDetailsAsync(userId, model);
            await _service.SaveChangesAsync();

            return Ok();
        }

        [HttpPatch("points/{userId}")]
        public async Task<IActionResult> Points(Guid userId, [FromBody]PointsUpdateModel model)
        {
            await _service.UpdatePointsAsync(userId, model);
            await _service.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _service.GetAllUserDetailsAsync();

            return Ok(users);
        }
    }
}