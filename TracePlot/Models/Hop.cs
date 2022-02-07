using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TracePlot.Models;

namespace TracePlot.Data
{
    public class Hop
    {
        [Key]
        public Guid HopId { get; set; }
        public string Address { get; set; }
        public List<ReplyTime> ReplyTimes { get; set; }
        public int HopNumber { get; set; }

        public double MedianReplyTime { get; set; }
        public double AverageReplyTime { get; set; }
        public long MinimumReplyTime { get; set; }
        public long MaximumReplyTime { get; set; }
        public double LowerQuartile { get; set; }
        public double HigherQuartile { get; set; }
        public Guid ParentID { get; set; }

        public Hop()
        {
            MedianReplyTime = 0;
            AverageReplyTime = 0;
            MinimumReplyTime = 0;
            MaximumReplyTime = 0;
            LowerQuartile = 0;
            HigherQuartile = 0;
        }

        private double CalculateQuartile(int n, float p)
        {
            float np = n * p;
            if (Math.Abs(np % 1) <= Double.Epsilon)
            {
                return (ReplyTimes[(int)np].Time + ReplyTimes[(int)np + 1].Time) / 2;
            }
            else
            {
                return ReplyTimes[(int)np + 1].Time;
            }
        }
        public void AddAndUpdateStatistics(long replyTime)
        {
            ReplyTime newReplyTime = new(replyTime, this.HopId);
            int index = ReplyTimes.BinarySearch(newReplyTime);
            if (index < 0)
            {
                ReplyTimes.Insert(~index, newReplyTime);
            }
            else
            {
                ReplyTimes.Insert(index, newReplyTime);
            }
            int replyTimesCount = ReplyTimes.Count;
            MaximumReplyTime = ReplyTimes[replyTimesCount - 1].Time;
            MinimumReplyTime = ReplyTimes[0].Time;

            if (replyTimesCount > 1)
            {
                //Always use the upper median for simplicity
                MedianReplyTime = ReplyTimes[(int)Math.Ceiling((double)replyTimesCount / 2)].Time;
            }
            AverageReplyTime += (replyTime - AverageReplyTime) / (double)replyTimesCount;

            //Need 4 values for calculating quartiles
            if (replyTimesCount >= 4)
            {
                LowerQuartile = CalculateQuartile(replyTimesCount - 1, 0.25f);
                HigherQuartile = CalculateQuartile(replyTimesCount - 1, 0.75f);
            }
        }
    }
}