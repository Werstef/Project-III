using KristMedv2.ApplicationLogic.DataModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KristMedv2.Models.Medics
{
    public class MedicAddTreatmentViewModel
    {
        public Guid AppointmentId { get; set; }
        public Guid ClientId { get; set; }
        public Guid MedicId { get; set; }
        public string Diagnosis { get; set; }
        public SelectList Equipments { get; set; }
        public Guid Equipment { get; set; }
        public int EquipmentUsageTime { get; set; }
        public SelectList Medications { get; set; }
        public Guid Medication { get; set; }
        public int MedicationQuantityUsed { get; set; }
    }
}
