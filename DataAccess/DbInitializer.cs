using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using HawaiiCrimeDetails.DataAccess;
using HawaiiCrimeDetails.APIHandlerManager;
using HawaiiCrimeDetails.Models;
using System.Collections.Generic;


namespace HawaiiCrimeDetails.DataAccess
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext dbContext)
        {
            APIHandler webHandler = new APIHandler();

            List<RootData> jsonData = webHandler.GetData();
            List<CrimeIncidents> crimeIncidents1 = new List<CrimeIncidents>();
            List<CMAgency> Agency = new List<CMAgency>();
            foreach (RootData data in jsonData)
            {
                var validIds = dbContext.crimeIncident.Select(t => t.objectid).ToList();
                if (!validIds.Contains(data.objectid))
                {
                    CrimeIncidents incident = new CrimeIncidents();
                    CMAgency agency = new CMAgency();
                    agency.kilonbr = data.kilonbr;
                    agency.cmid = data.cmid;
                    agency.cmagency = data.cmagency;
                    incident.objectid = data.objectid;
                    incident.cmid = data.cmid;
                    incident.Agency = agency;
                    incident.date = data.date;
                    incident.blockaddress = data.blockaddress;
                    incident.score = data.score;
                    incident.side = data.side;
                    incident.type = data.type;
                    incident.status = data.status;
                    incident.favorite = 0;
                    agency.crimeIncident = incident;
                    crimeIncidents1.Add(incident);
                    Agency.Add(agency);
                }
            }

            foreach (CrimeIncidents incident in crimeIncidents1)
            {
                // crimeIncident.RemoveRange();
                dbContext.crimeIncident.Add(incident);
                dbContext.SaveChanges();
            }
            foreach (CMAgency agency in Agency)
            {
               // dbContext.Agency.RemoveRange();
                dbContext.Agency.Add(agency);
                dbContext.SaveChanges();
            }
            dbContext.SaveChanges();

        }
    }
}