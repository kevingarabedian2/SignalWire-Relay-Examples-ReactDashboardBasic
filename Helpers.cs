using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Realtime_Dashboard_Example
{
    public static class Helpers
    {
        public class RealTimeEventArgs : EventArgs
        {
            public Guid ConsumptionId { get; set; }
            public string Type { get; set; }
            public Helpers.DashboardDataModel Payload { get; set; }
        }

        public static ConcurrentQueue<RealTimeEventArgs> data = new ConcurrentQueue<RealTimeEventArgs>();
        public class DashboardDataModel
        {
            public Guid Id { get; set; }
            public string ConnectId { get; set; }
            public string Direction { get; set; }
            public DateTime? StartDateTime { get; set; }
            public DateTime? EndDateTime { get; set; }
            public string StartDateTimeFormatted { get; set; }
            public string EndDateTimeFormatted { get; set; }
            public double Duration { get; set; }
            public string DurationFormatted { get; set; }
            public object From { get; set; }
            public object To { get; set; }         
            public string State { get; set; }
            public string Disposition { get; set; }
            public string Context { get; set; }
        }
    }
}
