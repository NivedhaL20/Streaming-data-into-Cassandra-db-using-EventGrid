using Meetup.WomenInTech.Services;
using Meetup.WomenInTech.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.EventGrid.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meetup.WomenInTech.Controllers.API
{
    [ApiController]
    [Route("[controller]")]
    public class EventGridController : ControllerBase
    {
        private readonly ILogger<EventGridController> _logger;
        private readonly IEventGridService _service;

        public EventGridController(ILogger<EventGridController> logger, IEventGridService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> PostEvent(List<EventGridEvent> input)
        {
            try
            {                
                var result = await _service.PostEventUsingSas(input);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }
        }
    }
}
