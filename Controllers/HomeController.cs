using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HawaiiCrimeDetails.Models;
using HawaiiCrimeDetails.APIHandlerManager;
using Newtonsoft.Json;
using HawaiiCrimeDetails.DataAccess;
using AutoMapper;
using HawaiiCrimeDetails.Models.ViewModel;
using System.Web;

namespace HawaiiCrimeDetails.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public ApplicationDbContext dbContext;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, IMapper mapper)
        {
            _logger = logger;
            dbContext = context;
            _mapper = mapper;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult IncidentDetails()
        {
            return View(dbContext.data.OrderByDescending(x => x.date).ToList());
        }
        [HttpGet]
        public IActionResult Incidents()
        {
            var type_counts = dbContext.data.GroupBy(a => a.type).OrderBy(group => group.Key).Select(group => Tuple.Create(group.Key, group.Count())).ToList();

            List<CountbyType> counttypes = new List<CountbyType>();
            foreach (var a in type_counts)
            {
                counttypes.Add(new CountbyType { type = a.Item1, count = a.Item2 });
            }
            counttypes.OrderBy(a => a.type);
            return View(counttypes);
        }

        [HttpGet("{id}")]
        public IActionResult IncidentDetails(string id)
        {
            id = id.Replace("%2F", "/");

            List<RootData> details = dbContext.data.Where(a => a.type == id).OrderByDescending(x => x.date).ToList();
          
            return View(details);
        }

        [HttpPost]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            RootData d = dbContext.data.Where(a => a.objectid == id).FirstOrDefault();
            return View(d);
        }

        
        public IActionResult Delete(string id)
        {
            string type = "";
            try
            {
                var del = dbContext.data.Where(a => a.objectid == id).FirstOrDefault();
                type = del.type;
                dbContext.data.Remove(del);
                dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
              //  return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("IncidentDetails", new { id = type });
        }
        public IActionResult AboutUs()
        {
            ViewData["Message"] = "About Us";
            return View();
        }

        public ActionResult Chart()
        {
            List<Chart> dataPoints = new List<Chart>();
            var type_counts = dbContext.data.GroupBy(a => a.type).OrderBy(group => group.Key).Select(group => Tuple.Create(group.Key, group.Count())).ToList();

            List<Chart> counttypes = new List<Chart>();
            foreach (var a in type_counts)
            {
                dataPoints.Add(new Chart(a.Item1, a.Item2));
            }
            dataPoints.OrderBy(a => a.Label);
            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);

            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
