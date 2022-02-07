using System.Runtime.CompilerServices;
using System.Net.NetworkInformation;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using TracePlot.Data;
using System;
using System.Collections;
using TracePlot.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace TracePlot
{
    public static class TraceRouteUtil
    {
        public static async Task TraceRouteStatistics(TraceRouteConfig config, TraceRouteDbContext context)
        {
            string Hostname = config.Hostname;
            int NumIterations = config.NumberOfIterations;
            int IntervalSize = config.IntervalSize;
            TraceRouteCollection trc = new()
            {
                TargetHostname = Hostname,
                NumberOfLoops = NumIterations,
                Start = DateTime.UtcNow,
                TraceRouteCollectionID = Guid.NewGuid(),
                Hops = new List<Hop>(),
                IntervalSize = IntervalSize
            };
            Dictionary<string, Hop> Hops = new();
            context.TraceRouteCollections.Add(trc);
            for (int i = 0; i < NumIterations; i++)
            {
                foreach (TraceRouteEntry tre in TraceRoute(Hostname))
                {
                    if (Hops.ContainsKey(tre.Address))
                    {
                        Hops[tre.Address].AddAndUpdateStatistics(tre.ReplyTime);
                    }
                    else
                    {
                        Hop h = new()
                        {
                            Address = tre.Address,
                            HopId = Guid.NewGuid(),
                            ReplyTimes = new List<ReplyTime>(),
                            HopNumber = tre.HopID,
                            ParentID = trc.TraceRouteCollectionID
                        };
                        h.AddAndUpdateStatistics(tre.ReplyTime);
                        Hops.Add(tre.Address, h);
                        trc.Hops.Add(h);
                        context.ReplyTimes.AddRange(h.ReplyTimes);
                    }
                }
                await Task.Delay(IntervalSize);
            }
            context.Hops.AddRange(trc.Hops);
            context.SaveChanges();
        }
        private static IEnumerable<TraceRouteEntry> TraceRoute(string Hostname)
        {
            Ping p = new();

            const int timeout = 1000;
            const string data = "";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            Stopwatch pingReplyTime = new();
            pingReplyTime.Reset();
            for (int ttl = 1; ttl < 64; ttl++)
            {
                PingOptions o = new(ttl, true);
                pingReplyTime.Start();
                PingReply reply = p.Send(Hostname, timeout, buffer, o);
                pingReplyTime.Stop();
                if (reply.Status == IPStatus.TtlExpired)
                {
                    yield return new TraceRouteEntry()
                    {
                        HopID = ttl,
                        Address = reply.Address == null ? "N/A" : reply.Address.ToString(),
                        ReplyTime = pingReplyTime.ElapsedMilliseconds,
                        Status = reply.Status
                    };
                    pingReplyTime.Reset();
                    continue;
                }

                if (reply.Status == IPStatus.TimedOut)
                {
                    yield return new TraceRouteEntry()
                    {
                        HopID = ttl,
                        Address = "N/A",
                        ReplyTime = timeout,
                        Status = reply.Status
                    };
                    pingReplyTime.Reset();
                    continue;
                }

                if (reply.Status == IPStatus.Success)
                {
                    yield return new TraceRouteEntry()
                    {
                        HopID = ttl,
                        Address = reply.Address.ToString(),
                        ReplyTime = pingReplyTime.ElapsedMilliseconds,
                        Status = reply.Status
                    };
                    pingReplyTime.Reset();
                }
                break;
            }
        }
    }
}