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
        public ApplicationDbContext db;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, IMapper mapper)
        {
            _logger = logger;
            db = context;
            _mapper = mapper;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult IncidentDetails()
        {
            return View(db.crimeIncident.OrderByDescending(x => x.date).ToList());
        }
        [HttpGet]
        public IActionResult Incidents()
        {
            var type_counts = db.crimeIncident.GroupBy(a => a.type).OrderBy(group => group.Key).Select(group => Tuple.Create(group.Key, group.Count())).ToList();

            List<CountbyType> counttypes = new List<CountbyType>();
            foreach (var a in type_counts)
            {
                counttypes.Add(new CountbyType { type = a.Item1, count = a.Item2 });
            }
            counttypes.OrderBy(a => a.type);
            return View(counttypes);
        }

        [HttpGet("{id}")]
        public IActionResult IncidentDetails(string id, string changesaved)
        {
            List<CrimeIncidents> details = db.crimeIncident.Where(a => a.type == id).OrderByDescending(x => x.date).ToList();

            List<Details> dets = new List<Details>();

            foreach(CrimeIncidents a in details)
            {
                a.Agency = db.Agency.Where(t => t.cmid == a.cmid).FirstOrDefault();
                Details d = new Details();
                d.objectid = a.objectid;
                d.blockaddress = a.blockaddress;
                d.cmid = a.cmid;
                d.cmagency = a.Agency.cmagency;
                d.date = a.date;
                d.type = a.type;
                d.status = a.status;

                dets.Add(d);
            }
            if(changesaved == "True")
                ViewBag.dbSuccessDel = 1;
            return View(dets);
        }

        [HttpPost]
        public IActionResult Create(Details d)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            CrimeIncidents a = db.crimeIncident.Where(a => a.objectid == id).FirstOrDefault();

            a.Agency = db.Agency.Where(t => t.cmid == a.cmid).FirstOrDefault();
            Details d = new Details();
            d.objectid = a.objectid;
            d.blockaddress = a.blockaddress;
            d.cmid = a.cmid;
            d.cmagency = a.Agency.cmagency;
            d.date = a.date;
            d.type = a.type;
            d.status = a.status;

            return View(d);
        }

        [HttpPost]
        public IActionResult Edit(Details d)
        {
            CrimeIncidents a = db.crimeIncident.Where(a => a.objectid == d.objectid).FirstOrDefault();
            var old = a;
            a.Agency = db.Agency.Where(t => t.cmid == a.cmid).FirstOrDefault();
            a.blockaddress = d.blockaddress;
            a.type = d.type;
            a.status = d.status;

            db.crimeIncident.Remove(old);
            db.SaveChanges();
            db.crimeIncident.Add(a);
            db.SaveChanges();
            
            ViewBag.dbUpdate = 1;

            Details det = new Details();
            det.objectid = a.objectid;
            det.blockaddress = a.blockaddress;
            det.cmid = a.cmid;
            det.cmagency = a.Agency.cmagency;
            det.date = a.date;
            det.type = a.type;
            det.status = a.status;

            return View(det);
        }
        public IActionResult Delete(int id)
        {
            string type = "";
            try
            {
                var del = db.crimeIncident.Where(a => a.objectid == id).FirstOrDefault();
                var cm = db.Agency.Where(a => a.cmid == del.cmid).FirstOrDefault();
                type = del.type;
                db.Agency.Remove(cm);
                db.crimeIncident.Remove(del);
                db.SaveChanges();
               
            }
            catch (Exception e)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
              //  return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("IncidentDetails", new { id = type, changesaved = true });
        }
        public IActionResult AboutUs()
        {
            ViewData["Message"] = "About Us";
            return View();
        }

        public ActionResult Chart()
        {
            List<Chart> dataPoints = new List<Chart>();
            var type_counts = db.crimeIncident.GroupBy(a => a.type).OrderBy(group => group.Key).Select(group => Tuple.Create(group.Key, group.Count())).ToList();

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
