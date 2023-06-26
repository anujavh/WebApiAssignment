using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiAssignemnt.Dto;
using WebApiAssignemnt.Services.MessageDetailService;

namespace WebApiAssignemnt.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendMessageController : ControllerBase
    {
        private readonly ISendMessageService _messageService;

        public SendMessageController(ISendMessageService sendMessageService)
        {
            _messageService = sendMessageService;
        }

        [HttpPost]
        public async Task<ActionResult<RespSendMessageDto>> SendMessageDetailsAsync(ReqSendMessageDto sendMessage)
        {
            var result = await _messageService.SendMessageDetailsAsync(sendMessage);
            return Ok(result);
        }
    }
}
