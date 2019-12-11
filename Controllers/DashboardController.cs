using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Realtime_Dashboard_Example.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Helpers.DashboardDataModel> Dashboard()
        {
            List<Helpers.DashboardDataModel> dataset = new List<Helpers.DashboardDataModel>();
            var records = Helpers.data.ToArray();
            foreach (var rt in records)
            {
                dataset.Add(rt.Payload);
            }
            return dataset;
        }
    }
}