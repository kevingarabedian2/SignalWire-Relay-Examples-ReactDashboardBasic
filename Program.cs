using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Realtime_Dashboard_Example.Consumers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Realtime_Dashboard_Example
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Task.Run(() => PrepareConsumer("phone", new List<string>() { "DashboardExample" }));

            CreateWebHostBuilder(args).Build().Run();
        }

        public static void PrepareConsumer(string type, List<string> contexts)
        {
            switch (type)
            {
                case "phone":
                    var pc = new PhoneConsumer
                    {
                        Project = "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX",
                        Token = "PTXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX",
                        Contexts = contexts
                    };
                    pc.RealTimeEvent += Pc_RealTimeEvent;
                    pc.Run();
                    break;
            }
        }
        private static void Pc_RealTimeEvent(object sender, Helpers.RealTimeEventArgs e)
        {
            Helpers.data.Enqueue(e);
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
