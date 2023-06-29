using Microsoft.AspNetCore.Mvc;
using WebApiAssignemnt.Dto;

namespace WebApiAssignemnt.Services.LogService
{
    public interface ICustomLogService
    {
        public Task<LogRequests> AddLogRequest(string IpAddress, string userName, HttpRequest request);

        public Task<List<LogRequests>> GetAllLogRequests(DateTime endTime, DateTime startTime);

        //void Information(string message);
        //void Warning(string message);
        //void Debug(string message);
        //void Error(string message);

    }
}
