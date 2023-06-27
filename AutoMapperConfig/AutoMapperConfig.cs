using AutoMapper;
using WebApiAssignemnt.Dto;

namespace WebApiAssignemnt.AutoMapperConfig
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig() {
            CreateMap<UserDetail, RespGetUserListDto>().ReverseMap();
            CreateMap<UserDetail, ReqGetUserListDto>().ReverseMap();
            CreateMap<UserDetail, ResUserRegistrationDto>().ReverseMap();            

            CreateMap<MessageDetails, ReqSendMessageDto>().ReverseMap();
            CreateMap<MessageDetails, RespSendMessageDto>().ReverseMap();


            CreateMap<MessageDetails, ReqGetMessageHistoryDto>().ReverseMap();
            CreateMap<MessageDetails, RespGetMessageHistoryDto>().ReverseMap();

            CreateMap<MessageDetails, ReqEditMessageDto>().ReverseMap();
            CreateMap<MessageDetails, RespEditMessageDto>().ReverseMap();

        }
    }
}
