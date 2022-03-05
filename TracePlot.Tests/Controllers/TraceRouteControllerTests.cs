using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Quartz;
using Quartz.Impl;
using TracePlot.Controllers;
using TracePlot.Data;
using TracePlot.Models;
using Xunit;
using Moq;
using Microsoft.EntityFrameworkCore;

namespace TracePlot.Tests.Controllers
{
    public class TraceRouteControllerTest
    {
        [Fact]
        public async Task BadRequestOnEmptyHostname()
        {
            TraceRouteConfig trc = new();

            var controller = new TraceRouteController(context: null, factory: null);
            var result = await controller.StartTraceRoute(trc);

            var badRequestActionResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequestActionResult.StatusCode);
            Assert.Equal(
                "{ Response = Please specify a valid hostname. }",
                badRequestActionResult.Value.ToString());
        }

        [Fact]
        public async Task BadRequestOnEmptyNumberOfIterations()
        {
            TraceRouteConfig trc = new();
            trc.Hostname = "www.test.com";

            var controller = new TraceRouteController(context: null, factory: null);
            var result = await controller.StartTraceRoute(trc);

            var badRequestActionResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequestActionResult.StatusCode);
            Assert.Equal(
                "{ Response = Please specify the number of iterations. }",
                badRequestActionResult.Value.ToString());
        }

        [Fact]
        public async Task OkOnCorrectInputData()
        {
            TraceRouteConfig trc = new();
            trc.Hostname = "www.microsoft.com";
            trc.NumberOfIterations = 5;
            trc.IntervalSize = 0;

            var options = new DbContextOptionsBuilder<TraceRouteDbContext>().UseInMemoryDatabase("TracerouteControllerTest").Options;

            ISchedulerFactory factory = new StdSchedulerFactory();
            using var context = new TraceRouteDbContext(options);
            var controller = new TraceRouteController(context: context, factory: factory);
            var result = await controller.StartTraceRoute(trc);

            var OkActionResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, OkActionResult.StatusCode);
            Assert.Equal(
                "{ Response = Successfully queued a traceroute to www.microsoft.com for 5 iterations with an interval of 0ms. }",
                OkActionResult.Value.ToString());
        }
    }
}
