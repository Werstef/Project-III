using System;
using System.ComponentModel.DataAnnotations;

namespace KristMedv2.ApplicationLogic.DataModels
{
    public class Equipment_Treatment
    {
        [Key]
        public Guid Id { get; set; }

        public int UsageTime { get; set; }

    }
}