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
using Quartz;
using TracePlot.Jobs;

namespace TracePlot.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TraceRouteController : ControllerBase
    {
        private readonly ISchedulerFactory _factory;
        private readonly TraceRouteDbContext _context;
        private readonly ILogger<TraceRouteController> _logger;

        public TraceRouteController(TraceRouteDbContext context, ILogger<TraceRouteController> logger, ISchedulerFactory factory)
        {
            _factory = factory;
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
            IScheduler scheduler = await _factory.GetScheduler();

            IJobDetail job = JobBuilder.Create<TraceRouteJob>().Build();
            job.JobDataMap.Add("Config", config);

            ISimpleTrigger trigger = (ISimpleTrigger)TriggerBuilder.Create().Build();

            await scheduler.ScheduleJob(job, trigger);

            return StatusCode(StatusCodes.Status200OK);
        }
    }
}