using System;
using System.ComponentModel.DataAnnotations;

namespace KristMedv2.ApplicationLogic.DataModels
{
    public class Medication_Treatment
    {

        [Key]
        public Guid Id { get; set; }

        public int QuantityUsed { get; set; }
    }
}