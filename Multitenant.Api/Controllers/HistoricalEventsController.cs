using Core.Entities;
using Core.Interfaces;
using Infrastructure.Caching;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Multitenant.Api.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
        public  string GetUserId()
        {
            var identity = (System.Security.Claims.ClaimsPrincipal)System.Threading.Thread.CurrentPrincipal;
            var principal = System.Threading.Thread.CurrentPrincipal as System.Security.Claims.ClaimsPrincipal;
            var userId = identity.Claims.Where(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier).Select(c => c.Value).SingleOrDefault();
            return userId;
        }
        [HttpPost("TrImport")]
        public async Task<IActionResult> TRImport(IFormFile file)
        {
            try
            {
                string fileContent = null;
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    fileContent = reader.ReadToEnd();
                }
                var result = JsonConvert.DeserializeObject<List<TrHistoricalEvent>>(fileContent);

                var newList = result.Select(i =>
                            new HistoricalEvents { DcKategori = i.DcKategori, DcOlay = i.DcOlay, DcZaman = i.DcZaman, Id = i.Id, UserId= Convert.ToInt64(GetUserId())});
                await _service.AddRange(newList.ToList());
                await _service.SaveChangesAsync();
                return StatusCode(200, "Success");
            }
            catch (Exception ex)
            {
                return StatusCode(301, ex.Message);
            }
          
        }
        [HttpPost("ITImport")]
        public async Task<IActionResult> ITImport(IFormFile file)
        {

            try
            {
                string fileContent = null;
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    fileContent = reader.ReadToEnd();
                }
                var result = JsonConvert.DeserializeObject<List<ITHistoricalEvent>>(fileContent);

                var newList = result.Select(i =>
                            new HistoricalEvents { DcKategori = i.DcCategoria, DcOlay = i.DcEvento, DcZaman = i.DcOrario, Id = i.Id, UserId = Convert.ToInt64(GetUserId()) });
                await _service.AddRange(newList.ToList());
                await _service.SaveChangesAsync();
                return StatusCode(200, "Success");
            }
            catch (Exception ex)
            {
                return StatusCode(301, ex.Message);
            }
        }

        [HttpGet]
        [Authorize()]
        public async Task<IActionResult> GetEventById(int id)
        {
            var cacheData = _cacheService.GetData<IEnumerable<HistoricalEventRequest>>(id.ToString());
            if (cacheData != null)
            {
                return Ok(cacheData);
            }
            var gethistory = await _service.GetByIdAsync(id);
            if (gethistory==null)
            {
                return new EmptyResult();
            }
            var expirationTime = DateTimeOffset.Now.AddMinutes(20.0);
            _cacheService.SetData<HistoricalEvents>(id.ToString(), gethistory, expirationTime);
            return Ok(gethistory);
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
