using WebApiAssignemnt.Dto;

namespace WebApiAssignemnt.Services.MessageDetailService
{
    public interface ISendMessageService
    {

        public Task<RespSendMessageDto> SendMessageDetailsAsync(ReqSendMessageDto sendMessage);

    }
}
