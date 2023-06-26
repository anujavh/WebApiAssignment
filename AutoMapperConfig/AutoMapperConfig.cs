using AutoMapper;
using WebApiAssignemnt.Dto;

namespace WebApiAssignemnt.AutoMapperConfig
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig() {
            CreateMap<UserDetail, RespGetUserListDto>().ReverseMap();
            CreateMap<UserDetail, getUserListDto>().ReverseMap();
            CreateMap<UserDetail, ResUserRegistrationDto>().ReverseMap();            

            CreateMap<MessageDetails, ReqSendMessageDto>().ReverseMap();
            CreateMap<MessageDetails, RespSendMessageDto>().ReverseMap();

        }
    }
}
