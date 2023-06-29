using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApiAssignemnt.Services.LogService;

namespace WebApiAssignemnt.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private readonly ICustomLogService _logService;

        public LogsController(ICustomLogService logService)
        {
            _logService = logService;
        }

        [HttpPost]
        public async Task<ActionResult<LogRequests>> AddLogRequest(string IpAddress , string UserName, HttpRequest request) //LogRequests logRequests)
        {
            var result = await _logService.AddLogRequest(IpAddress, UserName, request);
            return Ok(result);
        }


        // GET: api/Users
        [HttpGet("GetAllLogRequests")]
        [Authorize]
        public async Task<IActionResult> GetAllLogRequests(DateTime endTime, DateTime startTime)
        {
            if (endTime.ToString() == "01-01-0001 00:00:00")
                endTime = DateTime.Now;
            if(startTime.ToString() == null || startTime.ToString() == "01-01-0001 00:00:00") startTime = DateTime.Now.AddMinutes(-5);  
            //_logger.LogInformation("Post message method called "); 
            var res = await _logService.GetAllLogRequests(endTime, startTime);
            return Ok(res);
        }



    }
}
