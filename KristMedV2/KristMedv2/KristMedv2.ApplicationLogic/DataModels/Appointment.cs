using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KristMedv2.ApplicationLogic.DataModels
{
    public class Appointment
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public int Duration { get; set; }
        public Client? Client { get; set; }

        public Admin Admin { get; set; }

        public Treatment? Treatment { get; set; }

        public Medic Medic { get; set; }
    }
}