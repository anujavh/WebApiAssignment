using System.Security.Claims;
using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        private readonly ICustomLogService _logService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserDetailsController(IUserDetailService userDetailService, ILogger<UserDetailsController> logger, ICustomLogService logService, IHttpContextAccessor httpContextAccessor)
        {
            _userDetailService = userDetailService;
            _logger = logger;
            _logService = logService;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: api/Users
        [HttpGet("GetAllUsers")]
        [Authorize]
        public async Task<IActionResult> GetAllUsers()
        {
            var res = await _userDetailService.GetAllUsers();
            GetRemoteIpLogData( Request.HttpContext.Request);
            return Ok(res);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDetail>> GetUser(int id)
        {
            var result = await _userDetailService.GetById(id);
            GetRemoteIpLogData(Request.HttpContext.Request);
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

        private async void GetRemoteIpLogData(HttpRequest request)
        {
            string userName = await GetLoginUserName(); 
            string? ip_address = Request.HttpContext.Connection.RemoteIpAddress?.ToString();

            var result = await _logService.AddLogRequest(ip_address, userName, request);
            //var ip = logRequests;
            //return "Ok";

        }

        private async Task<int> GetUserID(UserDetail user)
        {
            await _userDetailService.GetById(user.Id);
            return 1;
        }
        private async Task<string> GetLoginUserName()
        {
            string username = _httpContextAccessor.HttpContext.User.Identity.Name;
            return username;
        }

    }
}
