﻿using Chess.ApiGateway.Api.ApiServices.UsersService.Models;
using Refit;
using System.Threading.Tasks;

namespace Chess.ApiGateway.Api.ApiServices.UsersService
{
    public interface IAuthService
    {
        [Post("/login")]
        Task<string> Login(LoginModel model);
    }
}