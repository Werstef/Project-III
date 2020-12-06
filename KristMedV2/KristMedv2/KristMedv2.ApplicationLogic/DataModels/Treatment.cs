using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KristMedv2.ApplicationLogic.DataModels
{
    public class Treatment
    {
        [Key]
        public Guid Id { get; set; }

        [Column(TypeName = "varchar(5000)")]
        public string Diagnosis { get; set; }

        public Client Client { get; set; }

        public Medic Medic { get; set; }

        //public ICollection<Appointment> Appointments { get; set; }

        public ICollection<Medication_Treatment>? Medications_Treatment { get; set; }

        public ICollection<Equipment_Treatment>? Equipments_Treatment { get; set; }

        public ICollection<Post>? Posts { get; set; }
    }
}