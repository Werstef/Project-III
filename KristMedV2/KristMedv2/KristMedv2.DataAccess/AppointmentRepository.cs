using KristMedv2.ApplicationLogic.Abstractions;
using KristMedv2.ApplicationLogic.DataModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KristMedv2.DataAccess
{
    public class AppointmentRepository : BaseRepository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(KristMedv2DbContext dbContext) : base(dbContext)
        {

        }

        public Appointment GetAppointmentByID(Guid Id)
        {

            var appointment = dbContext.Appointments
                            .Where(appointments => appointments.Id == Id)
                            .Include(appointment => appointment.Medic)
                            .Include(appointment => appointment.Admin)
                            .Include(appointment => appointment.Client)
                            .Include(appointment => appointment.Treatment)
                            .FirstOrDefault();
            return appointment;
        }

        public IEnumerable<Appointment> GetAllFutureAppointments()
        {
            return dbContext.Appointments
                             .Where(appointment => appointment.Date > DateTime.Now)
                             .Where(appointment => appointment.Client == null)
                             .Include(appointment => appointment.Medic)
                             .Include(appointment => appointment.Admin)
                             .Include(appointment => appointment.Client)
                             .Include(appointment => appointment.Treatment)
                             .AsEnumerable();
        }

        public IEnumerable<Appointment> GetAllAppointments()
        {
            return dbContext.Appointments
                             .Include(appointment => appointment.Medic)
                             .Include(appointment => appointment.Admin)
                             .Include(appointment => appointment.Client)
                             .Include(appointment => appointment.Treatment)
                             .AsEnumerable();
        }

        public IEnumerable<Appointment> GetAppointmentsFutureMedic(Guid Id)
        {
            return  dbContext.Appointments
                             .Where(appointment => appointment.Medic.UserId == Id)
                             .Where(appointment => appointment.Date > DateTime.Now)
                             .Where(appointment => appointment.Client != null)
                             .Include(appointment => appointment.Medic)
                             .Include(appointment => appointment.Admin)
                             .Include(appointment => appointment.Client)
                             .Include(appointment => appointment.Treatment)
                             .AsEnumerable();
        }

        public IEnumerable<Appointment> GetAppointmentsPastMedic(Guid Id)
        {
            return  dbContext.Appointments
                             .Where(appointment => appointment.Medic.UserId == Id)
                             .Where(appointment => appointment.Date < DateTime.Now)
                             .Where(appointment => appointment.Treatment == null)
                             .Where(appointment => appointment.Client != null)
                             .Include(appointment => appointment.Medic)
                             .Include(appointment => appointment.Admin)
                             .Include(appointment => appointment.Client)
                             .Include(appointment => appointment.Treatment)
                             .AsEnumerable();
        }
        public IEnumerable<Appointment> GetAppointmentsTreatmentMedic(Guid Id)
        {
            return dbContext.Appointments
                             .Where(appointment => appointment.Medic.UserId == Id)
                             .Where(appointment => appointment.Date < DateTime.Now)
                             .Where(appointment => appointment.Treatment != null)
                             .Include(appointment => appointment.Medic)
                             .Include(appointment => appointment.Admin)
                             .Include(appointment => appointment.Client)
                             .Include(appointment => appointment.Treatment)
                             .AsEnumerable();
        }

        public IEnumerable<Appointment> GetAppointmentsFutureMedicByLastNameForClient(string lastName)
        {
            return dbContext.Appointments
                             .Where(appointment => appointment.Medic.LastName == lastName || lastName == null)
                             .Where(appointment => appointment.Date > DateTime.Now)
                             .Where(appointment => appointment.Client == null)
                             .Include(appointment => appointment.Medic)
                             .Include(appointment => appointment.Admin)
                             .Include(appointment => appointment.Client)
                             .Include(appointment => appointment.Treatment)
                             .AsEnumerable();
        }

        public IEnumerable<Appointment> GetAppointmentsFutureClient(Guid Id)
        {
            return dbContext.Appointments
                             .Where(appointment => appointment.Client.UserId == Id)
                             .Where(appointment => appointment.Date > DateTime.Now)
                             .Include(appointment => appointment.Medic)
                             .Include(appointment => appointment.Admin)
                             .Include(appointment => appointment.Client)
                             .Include(appointment => appointment.Treatment)
                             .AsEnumerable();
        }

        public IEnumerable<Appointment> GetAppointmentsPastClient(Guid Id)
        {
            return dbContext.Appointments
                             .Where(appointment => appointment.Client.UserId == Id)
                             .Where(appointment => appointment.Date < DateTime.Now)
                             .Include(appointment => appointment.Medic)
                             .Include(appointment => appointment.Admin)
                             .Include(appointment => appointment.Client)
                             .Include(appointment => appointment.Treatment)
                             .AsEnumerable();
        }
    }
}
