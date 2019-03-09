using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WidgetApi.Models;

namespace WidgetApi.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class WidgetController : ControllerBase
    {

        ILogger<WidgetController> _logger;

        public WidgetController(ILogger<WidgetController> logger)
        {
            _logger = logger;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            _logger.LogDebug("Get was called");


            await Task.Delay(100); // simulate latency
            var items = new List<Widget>()
            {
                new Widget { ID = 1, Name = "Cog", Shape = "Square" },
                new Widget { ID = 2, Name = "Gear", Shape = "Round" },
                new Widget { ID = 3, Name = "Sprocket", Shape = "Octagonal" },
                new Widget { ID = 4, Name = "Pinion", Shape = "Triangular" }
            };

            return Ok(items);
        }
    }
}
