using KristMedv2.ApplicationLogic.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KristMedv2.Models.Admins
{
    public class AdminEquipmentViewModel
    {
        public IEnumerable<EquipmentType> EquipmentTypes { get; set; }
    }
}
