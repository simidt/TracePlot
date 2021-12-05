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

namespace TracePlot
{
    public static class TraceRouteUtil
    {
        public static void TraceRouteStatistics(string Hostname, int NumIterations, TraceRouteDbContext context)
        {
            Console.WriteLine(string.Format("Starting traceroute to {0} for {1} iterations", Hostname, NumIterations));
            TraceRouteCollection trc = new()
            {
                TargetHostname = Hostname,
                NumberOfLoops = NumIterations,
                Start = DateTime.UtcNow,
                TraceRouteCollectionID = Guid.NewGuid(),
                Hops = new List<Hop>()
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
            }
            context.Hops.AddRange(trc.Hops);
            context.SaveChanges();
        }
        private static IEnumerable<TraceRouteEntry> TraceRoute(string Hostname)
        {
            Ping p = new();

            const int timeout = 12000;
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