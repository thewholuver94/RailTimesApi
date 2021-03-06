using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using RailTimesApi.Models;
using Microsoft.AspNetCore.Authorization;

namespace RailTimesApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class RailApiController : ControllerBase
    {
        private readonly IHttpClientFactory _railClient;
        private readonly ILogger<RailApiController> _logger;

        public RailApiController(IHttpClientFactory clientFactory, ILogger<RailApiController> logger)
        {
            _railClient = clientFactory;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ResponseObject> Get()
        {
            HttpClient railClient = _railClient.CreateClient();
            //railClient.BaseAddress = new Uri(@"www.rail.co.il");
            string response = await railClient.GetStringAsync(@"http://www.rail.co.il/apiinfo/api/Plan/GetRoutes?OId={5200}&TId={5900}&Date={20200112}&Hour={0900}");
            ResponseObject resObj = JsonSerializer.Deserialize<ResponseObject>(response);
            return resObj;
        }

        [HttpGet("{user}")]
        public async Task<ResponseObject> Get(int user)
        {

            HttpClient railClient = _railClient.CreateClient();
            var response = await railClient.GetStringAsync($"http://www.rail.co.il/apiinfo/api/Plan/GetRoutes?OId={3500}&TId={5200}&Date={DateTime.Now.ToString("yyyyMMdd")}&Hour={DateTime.Now.Hour}{DateTime.Now.Minute}");
            ResponseObject resObj = JsonSerializer.Deserialize<ResponseObject>(response);
            return resObj;        
        }
    }
}