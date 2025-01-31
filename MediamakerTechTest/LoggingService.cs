using System.Text.Json;
using System;

namespace MediamakerTechTest
{
    public class LoggingService : ILoggingService
    {

        private readonly RequestDbContext _dbContext;

        public LoggingService(RequestDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void LogRequest( object request)
        {
            var logEntry = new RequestLog
            {
                
                RequestBody = JsonSerializer.Serialize(request),
                RequestTime = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss")
            };

            _dbContext.RequestLogs.Add(logEntry);
            _dbContext.SaveChanges();
        }


    }
}
