using System;
using System.Threading.Tasks;
using Quartz;
using TracePlot.Data;
using TracePlot.Models;

namespace TracePlot.Jobs
{
    /// <summary>
    /// Class <c>TraceRouteJob</c> describes a Quartz.NET job used to start the traceroute process with specific parameters.
    /// </summary>
    public class TraceRouteJob : IJob
    {
        private readonly TraceRouteDbContext _context;

        public TraceRouteJob(TraceRouteDbContext context)
        {
            _context = context;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            JobDataMap dataMap = context.JobDetail.JobDataMap;
            TraceRouteConfig config = (TraceRouteConfig)dataMap["Config"];

            await Task.Run(() => TraceRouteUtil.TraceRouteStatistics(config, _context));
        }
    }
}