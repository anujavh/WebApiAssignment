using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebApiAssignemnt.Dto;
using WebApiAssignemnt.Services.LogService;
using WebApiAssignemnt.Services.UserDetailService;


namespace WebApiAssignemnt.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDetailsController : ControllerBase
    {
        private readonly ILogger<UserDetailsController> _logger;

        private readonly IUserDetailService _userDetailService;
        private readonly ILogService _logService;

        public UserDetailsController(IUserDetailService userDetailService, ILogger<UserDetailsController> logger, ILogService logService)
        {
            _userDetailService = userDetailService;
            _logger = logger;
            _logService = logService;
        }

        // GET: api/Users
        [HttpGet("GetAllUsers")]
        [Authorize]
        public async Task<IActionResult> GetAllUsers()
        {
            Task<string> strIp = GetIpAddress();
            var res = await _userDetailService.GetAllUsers();
            return Ok(res);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<UserDetail>> GetUser(int id)
        {
            var result = await _userDetailService.GetById(id);
            if (result == null) { return NotFound("User doesnot exists."); }
            return Ok(result);
        }


        [HttpPost("register")]
        public async Task<ActionResult<ResUserRegistrationDto>> RegisterUser(ReqUserRegistrationDto user)
        {
            //string passwordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
            //user.PasswordHash = passwordHash;

            if (user.Email == null) { return this.ValidationProblem(); } //status code 400
            if (await UserEmailExists(user.Email))
            {
                //returns 409 status code
                return this.Conflict("Registration failed, the email id is already registered");
            }

            var result = await _userDetailService.AddUserDetailsAsync(user);
            //return CreatedAtAction("GetUser", new { id = user.UserId }, user);
            return Ok(result); //status code 200
        }

        [HttpPost("login")]
        [Authorize]
        public async Task<ActionResult<ResLoginUserDto>> LoginUser(ReqLoginUserDto loginUser)
        {
            if (loginUser.Email == null) { return this.ValidationProblem(); } //status code 400

            var result = await _userDetailService.GetLoginDetails(loginUser);
            //string token = CreateToken(result.e);
            return Ok(result); //status code 200
        }


        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _userDetailService.DelateUserDetails(id);

            if (result == null)
            {
                return NotFound("User not found");
            }

            return Ok(result);
        }

        private async Task<bool> UserEmailExists(string emailid)
        {
            var result = await _userDetailService.GetByEmailId(emailid);
            if (result) { return true; }//user exists
            return false;
        }

        private async Task<string> GetIpAddress()
        {
            string ip_address = Request.HttpContext.Connection.RemoteIpAddress?.ToString();

            var ip = await _logService.AddLogRequest(ip_address);
            return "Ok";

        }
    }
}
