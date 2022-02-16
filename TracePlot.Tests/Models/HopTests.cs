using System;
using System.Threading.Tasks;
using TracePlot.Data;
using Xunit;
using System.Linq;
namespace TracePlot.Tests.Models
{
    public class HopTest
    {
        [Fact]
        public void ReplyTimesAreSorted()
        {
            Hop h = new();
            Random rnd = new();
            for (int i = 0; i < 10; i++)
            {
                h.AddAndUpdateStatistics(rnd.Next(1, 10000));
                Assert.Equal(h.ReplyTimes.OrderBy(r => r.Time), h.ReplyTimes);

                // Quartiles are set after at least 4 entries exist and should be 0 otherwise
                if (h.ReplyTimes.Count >= 4)
                {
                    Assert.False(h.LowerQuartile == 0);
                    Assert.False(h.HigherQuartile == 0);
                }
                else
                {
                    Assert.Equal(0, h.LowerQuartile);
                    Assert.Equal(0, h.HigherQuartile);
                }
            }
        }
    }
}
