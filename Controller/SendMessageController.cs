using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApiAssignemnt.Dto;
using WebApiAssignemnt.Services.LogService;
using WebApiAssignemnt.Services.MessageDetailService;

namespace WebApiAssignemnt.Controller
{
    [Route("api/[controller]"), Authorize]
    [ApiController]
    public class SendMessageController : ControllerBase
    {
        private readonly ISendMessageService _messageService;
        private readonly ILogger<SendMessageController> _logger;

        private readonly ILogService _logService;
        public SendMessageController(ISendMessageService sendMessageService, ILogger<SendMessageController> logger, ILogService logService)
        {
            _messageService = sendMessageService;
            _logger = logger;
            _logService = logService;
        }

        [HttpPost]
        public async Task<ActionResult<RespSendMessageDto>> SendMessageDetailsAsync(ReqSendMessageDto sendMessage)
        {
            _logger.LogInformation("Post message method called ");
            var result = await _messageService.SendMessageDetailsAsync(sendMessage);
            if (result == null)
                return NotFound("User doesnot exists.");
            return Ok(result);
        }

        // GET: api/Users/5
        [HttpPut("EditMessage")]
        public async Task<ActionResult<RespSendMessageDto>> EditMessage(ReqEditMessageDto editMessage)
        {
            _logger.LogInformation("Edit message method called ");
            var result = await _messageService.UpdateMessage(editMessage);
            if (result == null) { return NotFound("User doesnot exists."); }
            return Ok(result);
        }

        [HttpDelete("deleteMessage")]
        public async Task<IActionResult> deleteMessage(int id)
        {
            try
            {
                var result = await _messageService.DeleteMessage(id);

                if (result == null)
                {
                    return NotFound("Message ID not found");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpGet("id")]
        public async Task<ActionResult<RespGetMessageHistoryDto>> GetMessageDetailsAsync(int id)
        {
            var result = await _messageService.GetMessageDetailsAsync(id);
            if (result == null) { return NotFound("Message details not found."); }
            return Ok(result);
        }


        //[HttpGet("{id},{beforeTime},{count},{sort}")]
        [HttpGet]
        public async Task<ActionResult<List<RespGetMessageHistoryDto>>> GetMessageDetailsListAsync(int id, DateTime beforeTime, int count, int sort)
        {
            _logger.LogInformation("Get message method called ");
            if (beforeTime.ToString() == null || beforeTime.ToString() == "01-01-0001 00:00:00")
                beforeTime = DateTime.Now;
            if (sort == 0)
                sort = 1;
            if (count == 0)
                count = 20;
            var result = await _messageService.GetMessageDetailsListAsync(id, beforeTime, count, sort);
            if (result == null) { return NotFound("Message details not found."); }
            return Ok(result);
        }

    }
}

