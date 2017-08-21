using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AzureRedisPlayArea.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace AzureRedisPlayArea.Controllers
{
    public class HomeController : Controller
    {
        private IDistributedCache _cache;

        public HomeController(IDistributedCache cache)
        {
            _cache = cache;
        }

        public IActionResult Index()
        {
            Audit audit = JsonConvert.DeserializeObject<Audit>(_cache.GetString("Audit") ?? "");

            if (audit == null)
            {
                audit = new Audit() { Name = "Important Audit", LastUpdated = DateTime.Now };

                var options = new DistributedCacheEntryOptions();
                options.SetSlidingExpiration(TimeSpan.FromSeconds(30));

                _cache.SetString("Audit", JsonConvert.SerializeObject(audit), options);
            }

            ViewData["Audit"] = audit;

            ViewData["DateTime"] = DateTime.Now.ToString(CultureInfo.CurrentCulture);

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
