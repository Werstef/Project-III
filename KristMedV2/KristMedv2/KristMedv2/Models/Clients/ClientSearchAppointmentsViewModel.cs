﻿using KristMedv2.ApplicationLogic.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KristMedv2.Models.Clients
{
    public class ClientSearchAppointmentsViewModel
    {
        public IEnumerable<Appointment> Appointments { get; set; }
    }
}
