using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using IdentityModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebApiAssignemnt.Dto;
using WebApiAssignemnt.Services.UserDetailService;

namespace WebApiAssignemnt.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDetailsController : ControllerBase
    {
        private readonly ILogger<UserDetailsController> _logger;
        private readonly IUserDetailService _userDetailService;
        public UserDetailsController(IUserDetailService userDetailService, ILogger<UserDetailsController> logger)
        {
            _userDetailService = userDetailService;
            _logger = logger;

        }

        // GET: api/Users
        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var res = await _userDetailService.GetAllUsers();
            return Ok(res);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDetail>> GetUser(int id)
        {
            var result = await _userDetailService.GetById(id);
            if (result == null) { return NotFound("User doesnot exists."); }
            return Ok(result);
        }

        //// PUT: api/Users/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutUser(int id, UserDetail user)
        //{
        //    if (id != user.UserId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(user).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!UserExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

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

    }
}
