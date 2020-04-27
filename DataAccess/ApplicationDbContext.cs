using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HawaiiCrimeDetails.Models;

namespace HawaiiCrimeDetails.DataAccess
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<RootData> data { get; set; }

        public DbSet<CrimeIncidents> crimeIncident { get; set; }

        public DbSet<CMAgency> Agency { get; set; }


    }
}