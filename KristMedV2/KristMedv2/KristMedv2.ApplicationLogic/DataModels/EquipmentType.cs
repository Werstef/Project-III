using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KristMedv2.ApplicationLogic.DataModels
{
    public class EquipmentType
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Manufacturer { get; set; }

        public int UsageTimeAvailable { get; set; }

        public ICollection<Equipment_Treatment>? Equipment_Treatments { get; set; }
    }
}