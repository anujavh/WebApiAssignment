using Microsoft.AspNetCore.Mvc;
using WebApiAssignemnt.Dto;

namespace WebApiAssignemnt.Services.MessageDetailService
{
    public interface ISendMessageService
    {

        public Task<RespSendMessageDto> SendMessageDetailsAsync(ReqSendMessageDto sendMessage);

        public Task<MessageDetails> DeleteMessage(int messageId);
        public Task<MessageDetails> UpdateMessage(ReqEditMessageDto editMessage);

        //public Task<RespGetMessageHistoryDto> GetMessageDetailsAsync(int receiverId);

        public Task<List<RespGetMessageHistoryDto>> GetMessageDetailsListAsync(int userId , DateTime beforeTime, int count, int sort);

        public Task<List<RespGetMessageHistoryDto>> GetMessageDetailsAsync(int userId);

    }
}
