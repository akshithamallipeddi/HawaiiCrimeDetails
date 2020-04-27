using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HawaiiCrimeDetails.Models
{
    public class RootData
    {
        [Key]
        public int objectid { get; set; }
        public string kilonbr { get; set; }
        public string blockaddress { get; set; }
        public string cmid { get; set; }
        public string cmagency { get; set; }
        public string date { get; set; }
        public string type { get; set; }
        public string status { get; set; }
        public string score { get; set; }
        public string side { get; set; }
    }
    public class CrimeIncidents
    {
        [Key]
        public int objectid { get; set; }
        public string cmid { get; set; }
        [ForeignKey("cmid")]
        public CMAgency Agency { get; set; }
        public string blockaddress { get; set; }
        public string date { get; set; }
        public string type { get; set; }
        public string status { get; set; }
        public string score { get; set; }
        public string side { get; set; }
        public int favorite { get; set; }
    }

    public class CMAgency
    {
        [Key]
        public string cmid { get; set; }
        public string kilonbr { get; set; }
        public string cmagency { get; set; }
        public CrimeIncidents crimeIncident { get; set; }
    }
}
