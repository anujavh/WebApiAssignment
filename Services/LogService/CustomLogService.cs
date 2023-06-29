using AutoMapper;
using WebApiAssignemnt.Services.MessageDetailService;
using WebApiAssignemnt.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Net;

namespace WebApiAssignemnt.Services.LogService
{
    public class CustomLogService : ICustomLogService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<SendMessageService> _logger;

        public CustomLogService(DataContext context, IMapper mapper, ILogger<SendMessageService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }


        //public async Tas5k<LogRequests> AddLogRequest(string IPAddress)
        public async Task<LogRequests> AddLogRequest(string IPAddress, string username, HttpRequest request)
        {
            try
            {
                LogRequests logRequests = new LogRequests();
                logRequests.IPAddress = IPAddress;
                logRequests.CreatedDateTime = DateTime.Now;
                logRequests.UserName = username;
                logRequests.RequestBody = request.ToString();

                var result = await _context.LogRequests.AddAsync(logRequests);
                await _context.SaveChangesAsync();

                return result.Entity;
            }
            catch (Exception ex) { _logger.LogError(ex.Message.ToString()); return null; }
        }


        public async Task<List<LogRequests>> GetAllLogRequests(DateTime endTime, DateTime startTime)
        {

            var result = await _context.LogRequests.Where(x => x.CreatedDateTime >= startTime && x.CreatedDateTime <= endTime).ToListAsync();
            return result;
        }

        //public void Debug(string message)
        //{
        //    _logger.Debug(message);
        //}

        //public void Error(string message)
        //{
        //    _logger.Error(message);
        //}

        //public void Information(string message)
        //{
        //    _logger.Info(message);
        //}

        //public void Warning(string message)
        //{
        //    _logger.Warn(message);
        //}
    }
}
