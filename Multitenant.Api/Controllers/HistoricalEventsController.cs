using Core.Interfaces;
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

        public HistoricalEventsController(IHistoricalEventService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetAsync(int id)
        {
            var productDetails = await _service.GetByIdAsync(id);
            return Ok(productDetails);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(HistoricalEventRequest request)
        {
            return Ok(await _service.CreateAsync(request.DcOlay, request.DcKategori, 1));
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
