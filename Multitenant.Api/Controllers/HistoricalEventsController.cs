using Core.Entities;
using Core.Interfaces;
using Infrastructure.Caching;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Multitenant.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoricalEventsController : ControllerBase
    {
        private readonly IHistoricalEventService _service;
        private readonly ICacheService _cacheService;

        public HistoricalEventsController(IHistoricalEventService service,  ICacheService cacheService)
        {
            _service = service;
            _cacheService=cacheService;
    }
        [HttpGet]
        public async Task<IActionResult> GetAsync(int id)
        {
            var cacheData = _cacheService.GetData<IEnumerable<HistoricalEventRequest>>("history");
            if (cacheData != null)
            {
                return Ok(cacheData);
            }
            var gethistory = await _service.GetByIdAsync(id);
            var expirationTime = DateTimeOffset.Now.AddMinutes(20.0);
            _cacheService.SetData<HistoricalEvents>("history", gethistory, expirationTime);
            return Ok(gethistory);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(HistoricalEvents request)
        {
            _cacheService.RemoveData("history");
            await _service.Create(request);
            return Ok( true);
        }
    }
    public class HistoricalEventRequest
    {
        public long ID { get; set; }
        public string DcZaman { get; set; }


        public string DcKategori { get; set; }


        public string DcOlay { get; set; }
    }
}
