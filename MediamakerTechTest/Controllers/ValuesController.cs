using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph;
using System;
using System.Net;
using static Microsoft.AspNetCore.Http.HttpRequest;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MediamakerTechTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ICalcService _calcService;
        private readonly ILoggingService _loggingService;
        private readonly RequestDbContext _dbContext;

        public ValuesController(ICalcService calcService, ILoggingService loggingService, RequestDbContext dbContext)
        {
            _calcService = calcService;
            _loggingService = loggingService;
            _dbContext = dbContext;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [Authorize]
        [HttpPost]
        [Route("api/ValuesController/CalculateSum")]
        public IActionResult CalculateSum([FromBody] UserCalcRequest request)
        {
            var response = new UserCalcResponse();
            response.answer = 0;

            try
            {
                _loggingService.LogRequest(request);

                response = _calcService.GetUserCalcResponse(request);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "Bad Request" });
            }
            return Ok(response);
        }


        [HttpGet]
        [Route("api/ValuesController/QueryLogs")]
        public async Task<ActionResult<IEnumerable<RequestLog>>> QueryLogTable()
        {
            var logs = await _dbContext.RequestLogs.ToListAsync();
            return Ok(logs);
        }


        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}