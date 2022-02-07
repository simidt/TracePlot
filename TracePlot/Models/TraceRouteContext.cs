using System;
using System.Collections.Generic;
using System.Security.Permissions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Net.NetworkInformation;
using System.ComponentModel.DataAnnotations;
using TracePlot.Models;

namespace TracePlot.Data
{
    public class TraceRouteDbContext : DbContext
    {
        public TraceRouteDbContext(DbContextOptions<TraceRouteDbContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite("Data Source=traceroutes.db");

        public DbSet<TraceRouteCollection> TraceRouteCollections { get; set; }
        public DbSet<Hop> Hops { get; set; }

        public DbSet<ReplyTime> ReplyTimes { get; set; }
    }
    public class TraceRouteCollection
    {
        [Key]
        public Guid TraceRouteCollectionID { get; set; }
        public string TargetHostname { get; set; }
        public DateTime Start { get; set; }
        public int NumberOfLoops { get; set; }

        public int IntervalSize { get; set; }
        public List<Hop> Hops { get; set; }
    }
}