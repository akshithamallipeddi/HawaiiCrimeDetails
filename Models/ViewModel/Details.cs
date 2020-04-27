using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace HawaiiCrimeDetails.Models.ViewModel
{
    public class Details
    {
        public int objectid { get; set; }
        [DisplayName("Block Address:")]
        public string blockaddress { get; set; }
        public string cmid { get; set; }
        [DisplayName("Crime management Agency")]
        public string cmagency { get; set; }
        [DisplayName("Date")]
        public string date { get; set; }
        [DisplayName("Type")]
        public string type { get; set; }
        [DisplayName("Status")]
        public string status { get; set; }
    }
}