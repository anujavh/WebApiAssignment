using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiAssignemnt.Dto;

namespace WebApiAssignemnt.Services.MessageDetailService
{
    public class SendMessageService : ISendMessageService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<SendMessageService> _logger;
        public SendMessageService(DataContext context, IMapper mapper, ILogger<SendMessageService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<RespSendMessageDto> SendMessageDetailsAsync(ReqSendMessageDto sendMessage)
        {
            try
            {
                var mapMessage = _mapper.Map<MessageDetails>(sendMessage);
                mapMessage.MessageTimestamp = DateTime.Now;
                var result = await _context.MessageDetails.AddAsync(mapMessage);
                await _context.SaveChangesAsync();

                return _mapper.Map<RespSendMessageDto>(result.Entity);
            }
            catch (Exception ex) { _logger.LogError(ex.Message.ToString()); return null; }
        }

        public async Task<MessageDetails> DeleteMessage(int messageId)
        {
            try
            {
                var messageDetails = await _context.MessageDetails.FirstOrDefaultAsync(x => x.receiverId == messageId);

                if (messageDetails != null)
                {
                    _context.MessageDetails.Remove(messageDetails);
                    _context.SaveChanges();
                    return messageDetails;
                }
                else
                    return null;
            }
            catch (Exception ex) { _logger.LogError(ex.Message.ToString()); return null; }
        }

        public async Task<MessageDetails> UpdateMessage(ReqEditMessageDto editMessage)
        {
            try
            {
                var reqDetails = await _context.MessageDetails.FirstOrDefaultAsync(x => x.MessageId == editMessage.MessageId);
                if (reqDetails == null)
                    return null;

                reqDetails.MessageContent = editMessage.MessageContent.Replace("\r\n", "\n");
                reqDetails.MessageTimestamp = DateTime.Now;
                _mapper.Map<MessageDetails>(reqDetails);
                await _context.SaveChangesAsync();
                return reqDetails;
            }
            catch (Exception ex) { _logger.LogError(ex.Message.ToString()); return null; }
        }


        public async Task<List<RespGetMessageHistoryDto>> GetMessageDetailsListAsync(int userId, DateTime before, int count, int sort)
        {
            try
            {
                if (before.ToString() == "01-01-0001 00:00:00")
                    before = DateTime.Now;
                var result = await _context.MessageDetails.Where(x => x.senderId == userId && before <= DateTime.Now).ToListAsync();

                List<RespGetMessageHistoryDto> listHistory = new List<RespGetMessageHistoryDto>();
                if (result != null)
                    listHistory = _mapper.Map<List<RespGetMessageHistoryDto>>(result);


                int maxCount = 20;
                if (count > maxCount)
                {
                    return null; //Ok("Max user can fetch 20 messages from conversatiuon history.");
                }


                if (sort == 1)
                {
                    var sortResult = listHistory.OrderBy(i => i.messageTimestamp).Take(count).ToList();
                    return _mapper.Map<List<RespGetMessageHistoryDto>>(sortResult);
                }
                else
                {
                    var sortResult1 = listHistory.OrderByDescending(i => i.messageTimestamp).Take(count).ToList();
                    return _mapper.Map<List<RespGetMessageHistoryDto>>(sortResult1);
                }
            }
            catch (Exception ex) { _logger.LogError(ex.Message.ToString()); return null; }
        }



        public async Task<List<RespGetMessageHistoryDto>> GetMessageDetailsAsync(int userId)
        {
            try
            {
                var result = await _context.MessageDetails.Where(x => x.senderId == userId).FirstOrDefaultAsync();
                List<RespGetMessageHistoryDto> listHistory = new List<RespGetMessageHistoryDto>();

                return _mapper.Map<List<RespGetMessageHistoryDto>>(result);
            }
            catch (Exception ex) { _logger.LogError(ex.Message.ToString()); return null; }

        }
    }
}