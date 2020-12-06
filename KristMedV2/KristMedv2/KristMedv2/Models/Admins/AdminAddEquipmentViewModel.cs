using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KristMedv2.Models.Admins
{
    public class AdminAddEquipmentViewModel
    {
        public string Name { get; set; }

        public string Manufacturer { get; set; }

        public int UsageTime { get; set; }

        //public DateTime ExpirationDate { get; set; }
    }
}
