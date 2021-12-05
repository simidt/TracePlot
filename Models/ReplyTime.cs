using System;
using System.ComponentModel.DataAnnotations;
using TracePlot.Data;

namespace TracePlot.Models
{
    public class ReplyTime : IComparable<ReplyTime>
    {   
        [Key]
        public Guid ID { get; set; }
        public long Time { get; set; }

        public Guid HopID { get; set; }

        public ReplyTime(long Time, Guid HopID)
        {
            this.ID = Guid.NewGuid();
            this.Time = Time;
            this.HopID = HopID;
        }

        public int CompareTo(ReplyTime other)
        {
            if (other == null) return 1;
            return Time.CompareTo(other.Time);
        }
    }
}