using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HawaiiCrimeDetails.Models.ViewModel
{
    public class Details
    {
        public int objectid { get; set; }
        public string blockaddress { get; set; }
        public string cmid { get; set; }
        public string cmagency { get; set; }
        public string date { get; set; }
        public string type { get; set; }
        public string status { get; set; }
    }
}