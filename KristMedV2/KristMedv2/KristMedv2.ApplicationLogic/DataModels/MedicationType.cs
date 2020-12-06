using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KristMedv2.ApplicationLogic.DataModels
{
    public class MedicationType
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Manufacturer { get; set; }

        public int QuantityStock { get; set; }

        public DateTime ExpirationDate { get; set; }

        public ICollection<Medication_Treatment>? Medication_Treatments { get; set; }
    }
}