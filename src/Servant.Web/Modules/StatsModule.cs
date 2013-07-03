﻿using System.Linq;
using Nancy;
using Servant.Web.Helpers;

namespace Servant.Web.Modules
{
    public class StatsModule: BaseModule
    {
        public StatsModule() : base("/stats/")
        {
            Get["/"] = p => {
                var serverStats = new Business.Objects.Reporting.ServerStats();
                
                //serverStats.TotalRequests = logEntryService.GetTotalCount();
                serverStats.DataRecieved = "Not available";
                serverStats.DataSent = "Not available";
                serverStats.TotalSites = SiteManager.GetSites().Count();


                //serverStats.AverageResponeTime = (int)logEntryService.GetAverageResponseTime();

                //serverStats.TotalErrors = applicationErrorService.GetTotalCount();
                serverStats.UnusedApplicationPools = ApplicationPoolHelper.GetUnusedApplicationPools().Count();
                Model.ServerStats = serverStats;
                return View["Index", Model];
            };

            Get["/cleanupapplicationpools/"] = p => {
                Model.UnusedApplicationPools = ApplicationPoolHelper.GetUnusedApplicationPools();
                return View["CleanupApplicationPools", Model];
            };

            Post["/cleanupapplicationpools/"] = p => {
                var applicationPools = Request.Form.ApplicationPools.ToString().Split(',');

                foreach(var applicationPool in applicationPools)
                {
                    ApplicationPoolHelper.Delete(applicationPool);
                }

                return Response.AsRedirect("/stats/");
            };

        }
    }
}