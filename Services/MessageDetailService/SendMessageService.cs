using AutoMapper;
using WebApiAssignemnt.Dto;

namespace WebApiAssignemnt.Services.MessageDetailService
{
    public class SendMessageService : ISendMessageService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public SendMessageService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<RespSendMessageDto> SendMessageDetailsAsync(ReqSendMessageDto sendMessage)
        {
            var mapMessage = _mapper.Map<MessageDetails>(sendMessage);
            mapMessage.MessageTimestamp = DateTime.Now;
            var result = await _context.MessageDetails.AddAsync(mapMessage);
            await _context.SaveChangesAsync();

            return _mapper.Map<RespSendMessageDto>(result.Entity);
        }


    }
}
