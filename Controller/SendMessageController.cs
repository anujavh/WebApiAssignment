using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text;
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

        private readonly ICustomLogService _logService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SendMessageController(ISendMessageService sendMessageService, ILogger<SendMessageController> logger, ICustomLogService logService, IHttpContextAccessor httpContextAccessor)
        {
            _messageService = sendMessageService;
            _logger = logger;
            _logService = logService;
            _httpContextAccessor = httpContextAccessor;
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



            var request = Request.HttpContext.Request;
            var bodyStr = "";
            var req = Request.HttpContext.Request;

            //// Allows using several time the stream in ASP.Net Core
            ////req.EnableRewind();

            //// Arguments: Stream, Encoding, detect encoding, buffer size 
            //// AND, the most important: keep stream opened
            //using (StreamReader reader
            //          = new StreamReader(req.Body, Encoding.UTF8, true, 1024, true))
            //{
            //    bodyStr = reader.ReadToEnd();
            //}

            //// Rewind, so the core is not lost when it looks at the body for the request
            //req.Body.Position = 0;

            //// Do whatever works with bodyStr here

            GetRemoteIpLogData(request);
            if (result == null) { return NotFound("User doesnot exists."); }
            return Ok(result);
        }

        [HttpDelete("deleteMessage")]
        public async Task<IActionResult> deleteMessage(int id)
        {
            try
            {
                var result = await _messageService.DeleteMessage(id);
                GetRemoteIpLogData(Request.HttpContext.Request);
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

        //[HttpGet("id")]
        //public async Task<ActionResult<RespGetMessageHistoryDto>> GetMessageDetailsAsync(int id)
        //{
        //    var result = await _messageService.GetMessageDetailsAsync(id);

        //    GetRemoteIpLogData(Request.HttpContext.Request);
        //    if (result == null) { return NotFound("Message details not found."); }

        //    return Ok(result);
        //}


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
            GetRemoteIpLogData(Request.HttpContext.Request);
            if (result == null) { return NotFound("Message details not found."); }
            return Ok(result);
        }

        private async void GetRemoteIpLogData(HttpRequest request)
        {
            string username = await GetLoginUserName();
            string? ip_address = Request.HttpContext.Connection.RemoteIpAddress?.ToString();//This will return ::1 if executing api on same computer

            var result = await _logService.AddLogRequest(ip_address, username, request);
            // var ip = logRequests;
            //return "Ok";
        }

        private async Task<string> GetLoginUserName()
        {
            string username = _httpContextAccessor.HttpContext.User.Identity.Name;
            if (string.IsNullOrEmpty(username))
                return username = "";
            return username.ToString();
        }


    }
}

