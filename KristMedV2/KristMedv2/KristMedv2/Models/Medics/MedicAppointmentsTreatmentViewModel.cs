﻿using KristMedv2.ApplicationLogic.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KristMedv2.Models.Medics
{
    public class MedicAppointmentsTreatmentViewModel
    {
        public IEnumerable<Appointment> AppointmentsTreatment { get; set; }
    }
}
