using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBackend.Dto;
using WebBackend.Infrastructure;
using WebBackend.Interfaces;

namespace WebBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            return Ok(_userService.GetUser(id));
        }

        [HttpPost]
        public IActionResult CreateUser(RegisterDto registerDto)
        {
            return Ok(_userService.CreateUser(registerDto));
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDto loginDto)
        {
            return Ok(_userService.Login(loginDto));
        }

        [HttpPost("verify")]
        public IActionResult VerifyPostal(int id, bool yes)
        {
            _userService.VerifyPostal(id, yes);
            return Ok();
        }

        [HttpPut]
        public IActionResult ChangeUser(RegisterDto registerDto)
        {
            return Ok(_userService.ChangeUser(registerDto));
        }
    }
}
