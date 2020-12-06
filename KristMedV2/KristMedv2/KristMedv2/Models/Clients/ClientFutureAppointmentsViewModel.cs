using KristMedv2.ApplicationLogic.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KristMedv2.Models.Clients
{
    public class ClientFutureAppointmentsViewModel
    {
        public IEnumerable<Appointment> FutureAppointments { get; set; }
    }
}
