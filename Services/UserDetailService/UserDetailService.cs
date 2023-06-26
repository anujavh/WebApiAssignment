using AutoMapper;
using IdentityModel;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApiAssignemnt.Dto;

namespace WebApiAssignemnt.Services.UserDetailService
{
    public class UserDetailService : IUserDetailService
    {
        //public static List<UserDetail> userDetails = new List<UserDetail>()
        //{
        //new UserDetail {  Id = 1, UserName="Anuja", Email="anuja@pro.com", PasswordHash="1234" }
        //};
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserDetailService(DataContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<UserDetail?> GetById(int id)
        {
            var result = await _context.UserDetails.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
                return null;
            return result;
        }

        public async Task<bool> GetByEmailId(string emailid)
        {
            var result = await _context.UserDetails.FirstOrDefaultAsync(x => x.Email == emailid);
            if (result == null)
                return false;
            return true;
        }

        public async Task<ResLoginUserDto> GetLoginDetails(ReqLoginUserDto loginUserDto)
        {
            ResLoginUserDto resUser = new();
            var result = await _context.UserDetails.FirstOrDefaultAsync(x => x.Email == loginUserDto.Email
            && x.PasswordHash == loginUserDto.PasswordHash);

            if (result == null)
                return null;

            else
            {
                resUser.token = CreateToken(result.Id);
                resUser.userProfile = _mapper.Map<ResUserRegistrationDto>(result);
                return resUser;
            }
        }

        public async Task<List<RespGetUserListDto>?> GetAllUsers()
        {
            var result = await _context.UserDetails.ToListAsync();
            if (result != null)
            {
                return _mapper.Map<List<RespGetUserListDto>>(result);
            }
            else
            {
                return null;
            }
        }

        public async Task<ResUserRegistrationDto> AddUserDetailsAsync(ReqUserRegistrationDto userRegistrationDto)
        {
            UserDetail userDetail = new UserDetail();
            ResUserRegistrationDto resUser = new ResUserRegistrationDto();

            userDetail.PasswordHash = userRegistrationDto.PasswordHash;
            userDetail.Email = userRegistrationDto.Email;
            userDetail.UserName = userRegistrationDto.UserName;

            var result = await _context.UserDetails.AddAsync(userDetail);
            await _context.SaveChangesAsync();

            resUser.UserName = result.Entity.UserName;
            resUser.Email = result.Entity.Email;
            resUser.Id = result.Entity.Id;

            return resUser;
        }

        public async Task<UserDetail> DelateUserDetails(int id)
        {
            var userDetails = await _context.UserDetails.FirstOrDefaultAsync(x => x.Id == id);
            if (userDetails == null)
            { return null; }

            else
            {
                _context.UserDetails.Remove(userDetails);
                _context.SaveChanges();
                return userDetails;
            }

        }

        public async Task<UserDetail> UpdateUserDetails(int id, UserDetail requestUserDetails)
        {
            var userdtl = await _context.UserDetails.FirstOrDefaultAsync(x => x.Id == id);
            if (userdtl is null) { return null; }

            userdtl.UserName = requestUserDetails.UserName;
            userdtl.Email = requestUserDetails.Email;
            userdtl.PasswordHash = requestUserDetails.PasswordHash;

            return userdtl;
        }


        //public async Task<UserDetail> DelateUserDetails(int id)
        //{
        //    var userDetails = await _context.UserDetails.FirstOrDefaultAsync(x => x.Id == id);
        //    if (userDetails == null)
        //    { return null; }

        //    else
        //    {
        //        _context.UserDetails.Remove(userDetails);
        //        return userDetails;
        //    }

        //}

        //public async Task<UserDetail> UpdateUserDetails(int id, UserDetail requestUserDetails)
        //{
        //    var userdtl = await _context.UserDetails.FirstOrDefaultAsync(x => x.Id == id);
        //    if (userdtl is null) { return null; }

        //    userdtl.UserName = requestUserDetails.UserName;
        //    userdtl.Email = requestUserDetails.Email;
        //    userdtl.PasswordHash = requestUserDetails.PasswordHash;

        //    return userdtl;
        //}


        private string CreateToken(int id)
        {
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration.GetSection("JWTSettings:Token").Value!));

            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(new List<Claim>() { new Claim(JwtClaimTypes.Id, id.ToString()) }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature)
            };

            JwtSecurityTokenHandler tokenHandler = new();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token); //to verify the generated JWT token- go to https://jwt.io/ add jwt encoded token and decode it.
        }
    }
}
