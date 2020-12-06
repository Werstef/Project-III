using KristMedv2.ApplicationLogic.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace KristMedv2.ApplicationLogic.Abstractions
{
    public interface IAppointmentRepository: IRepository<Appointment>
    {
        public Appointment GetAppointmentByID(Guid Id);
        public IEnumerable<Appointment> GetAllAppointments();
        public IEnumerable<Appointment> GetAllFutureAppointments();
        public IEnumerable<Appointment> GetAppointmentsFutureMedic(Guid Id);
        public IEnumerable<Appointment> GetAppointmentsPastMedic(Guid Id);
        public IEnumerable<Appointment> GetAppointmentsTreatmentMedic(Guid Id);
        public IEnumerable<Appointment> GetAppointmentsFutureMedicByLastNameForClient(string lastName);
        public IEnumerable<Appointment> GetAppointmentsFutureClient(Guid Id);
        public IEnumerable<Appointment> GetAppointmentsPastClient(Guid Id);

    }
}
