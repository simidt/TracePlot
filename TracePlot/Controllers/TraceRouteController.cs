using System;
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

        public TraceRouteController(TraceRouteDbContext context, ISchedulerFactory factory)
        {
            _factory = factory;
            _context = context;
        }
        /// <summary>
        /// Handles GET requests to the traceroute endpoint.
        /// </summary>
        /// <returns>All <c>TraceRouteCollections</c> including their respective hops and statistics.</returns>
        [HttpGet]
        public IEnumerable<TraceRouteCollection> GetTraceRoutes()
        {
            return _context.TraceRouteCollections.Include(trc => trc.Hops).ToList();
        }
        /// <summary>
        /// Handles POST requests to the traceroute endpoint and starts a new traceroute on receiving a non-malformed request.
        /// </summary>
        /// <param name="config">A <c>TraceRouteConfig</c> instance containing the hostname, number of iterations and interval size of the traceroute to be started</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> StartTraceRoute([FromBody] TraceRouteConfig config)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(config.Hostname))
                {
                    return BadRequest(error: new { Response = "Please specify a valid hostname." });
                }
                if (config.NumberOfIterations == 0)
                {
                    return BadRequest(error: new { Response = "Please specify the number of iterations." });
                }

                IScheduler scheduler = await _factory.GetScheduler();

                IJobDetail job = JobBuilder.Create<TraceRouteJob>().Build();
                job.JobDataMap.Add("Config", config);

                ISimpleTrigger trigger = (ISimpleTrigger)TriggerBuilder.Create().Build();

                await scheduler.ScheduleJob(job, trigger);

                return Ok(value: new
                {
                    Response = $"Successfully queued a traceroute to {config.Hostname} for {config.NumberOfIterations} iterations with an interval of {config.IntervalSize}ms."
                });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, value: "Traceroute process failed");
            }
        }
    }
}