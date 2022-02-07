using System.Net.NetworkInformation;
namespace TracePlot
{
    public class TraceRouteEntry
    {
        public int HopID {get;set;}
        public string Address {get;set;}

        public long ReplyTime {get;set;}

        public IPStatus Status {get;set;}

        public override string ToString(){
            return string.Format("{0} | {1} | {2}", HopID, Address, Status == IPStatus.TimedOut ? "Request timed out" : ReplyTime.ToString() + " ms");
        }
    }
}