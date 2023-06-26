using WebApiAssignemnt.Dto;

namespace WebApiAssignemnt.Services.UserDetailService
{
    public interface IUserDetailService
    {
        Task<List<RespGetUserListDto>?> GetAllUsers();
        public Task<UserDetail?> GetById(int id);
        public Task<bool> GetByEmailId(string emailId);
        public Task<ResLoginUserDto> GetLoginDetails(ReqLoginUserDto loginUserDto);

        public Task<ResUserRegistrationDto> AddUserDetailsAsync(ReqUserRegistrationDto userDetail);
        public Task<UserDetail> UpdateUserDetails(int id, UserDetail requestUserDetails);
        public Task<UserDetail> DelateUserDetails(int id);

    }
}
