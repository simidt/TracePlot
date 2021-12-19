using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TracePlot.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TracePlot.Models;

namespace TracePlot.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TraceRouteController : ControllerBase
    {
        private readonly TraceRouteDbContext _context;
        private readonly ILogger<TraceRouteController> _logger;

        public TraceRouteController(TraceRouteDbContext context, ILogger<TraceRouteController> logger)
        {
            _context = context;
            _logger = logger;
        }
        [HttpGet]
        public IEnumerable<TraceRouteCollection> GetTraceRoute()
        {
            return _context.TraceRouteCollections.Include(trc => trc.Hops).ToList();
        }
        [HttpPost]
        public async Task<IActionResult> StartTraceRoute([FromBody] TraceRouteConfig config)
        {
            //TODO: Queue these requests and schedule them for a later time; return status 200 if the queuing is done
            await Task.Run(() => TracePlot.TraceRouteUtil.TraceRouteStatistics(config, _context));
            return StatusCode(StatusCodes.Status200OK);
        }

    }
}