using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KristMedv2.Models.Admins
{
    public class AdminAddAppointmentViewModel
    {
        public SelectList Medics { get; set; }

        public Guid Medic { get; set; }

        public DateTime Date { get; set; }

        public int Duration { get; set; }
    }
}
