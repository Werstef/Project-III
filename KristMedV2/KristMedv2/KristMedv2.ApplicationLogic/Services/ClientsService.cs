using KristMedv2.ApplicationLogic.Abstractions;
using KristMedv2.ApplicationLogic.DataModels;
using KristMedv2.ApplicationLogic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KristMedv2.ApplicationLogic.Services
{
    public class ClientsService
    {
        IClientRepository clientRepository;
        IContactMessageRepository contactMessageRepository;
        IMedicationTypeRepository medicationTypeRepository;
        IMedicRepository medicRepository;
        IAppointmentRepository appointmentRepository;
        IEquipmentTypeRepository equipmentTypeRepository;
        ITreatmentRepository treatmentRepository;
        IMedication_TreatmentRepository medication_TreatmentRepository;
        IEquipment_TreatmentRepository equipment_TreatmentRepository;

        public ClientsService(IClientRepository clientRepository, IContactMessageRepository contactMessageRepository,
            IMedicationTypeRepository medicationTypeRepository, IMedicRepository medicRepository, 
            IAppointmentRepository appointmentRepository, IEquipmentTypeRepository equipmentTypeRepository,
            ITreatmentRepository treatmentRepository, IMedication_TreatmentRepository medication_TreatmentRepository, 
            IEquipment_TreatmentRepository equipment_TreatmentRepository)
        {
            this.clientRepository = clientRepository;
            this.contactMessageRepository = contactMessageRepository;
            this.medicationTypeRepository = medicationTypeRepository;
            this.medicRepository = medicRepository;
            this.appointmentRepository = appointmentRepository;
            this.equipmentTypeRepository = equipmentTypeRepository;
            this.treatmentRepository = treatmentRepository;
            this.medication_TreatmentRepository = medication_TreatmentRepository;
            this.equipment_TreatmentRepository = equipment_TreatmentRepository;
        }

        public Client CreateClient(string userId, string firstName, string lastName, string email, string phoneNo)
        {
            var client = new Client
            {
                UserId = Guid.Parse(userId),
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PhoneNo = phoneNo
            };
            clientRepository.Add(client);
            return client;
        }

        public Client GetClientByUserId(string userId)
        {
            Guid userIdGuid = Guid.Empty;
            if (!Guid.TryParse(userId, out userIdGuid))
            {
                throw new Exception("Invalid Guid Format");
            }

            var client = clientRepository.GetClientByUserId(userIdGuid);
            if (client == null)
            {
                throw new EntityNotFoundException(userIdGuid);
            }

            return client;
        }

        public IEnumerable<ContactMessage> GetContactMessagesForClient(string userId)
        {
            Guid userIdGuid = Guid.Empty;
            if (!Guid.TryParse(userId, out userIdGuid))
            {
                throw new Exception("Invalid Guid Format");
            }

            return contactMessageRepository.GetAll()
                            .Where(message => message.Client != null && message.Client.UserId == userIdGuid)
                            .AsEnumerable();
        }

        public void AddContactMessage(string userId, string fullName, string email, string description)
        {
            Guid userIdGuid = Guid.Empty;
            if (!Guid.TryParse(userId, out userIdGuid))
            {
                throw new Exception("Invalid Guid Format");
            }
            var client = clientRepository.GetClientByUserId(userIdGuid);
            if (client == null)
            {
                throw new EntityNotFoundException(userIdGuid);
            }

            contactMessageRepository.Add(new ContactMessage()
            {
                Id = Guid.NewGuid(),
                FullName = fullName,
                Email = email,
                MessageText = description,
                Client = client
            });
        }


        public void DeleteContactMessage(string contactId)
        {
            Guid contactIdGuid = Guid.Empty;
            if (!Guid.TryParse(contactId, out contactIdGuid))
            {
                throw new Exception("Invalid Guid Format");
            }
            var contactMessage = contactMessageRepository.GetById(contactIdGuid);
            contactMessageRepository.Delete(contactMessage);
        }

        public Client GetClientById(string clientId)
        {
            Guid clientIdGuid = Guid.Empty;
            if (!Guid.TryParse(clientId, out clientIdGuid))
            {
                throw new Exception("Invalid Guid Format");
            }

            var client = clientRepository.GetClientByUserId(clientIdGuid);
            if (client == null)
            {
                throw new EntityNotFoundException(clientIdGuid);
            }

            return client;

        }

        public void EditAccount(Guid userId, string firstName, string lastName,
                                string phoneNo)
        {

            var client = clientRepository.GetClientByUserId(userId);

            client.FirstName = firstName;
            client.LastName = lastName;
            client.PhoneNo = phoneNo;

            clientRepository.Update(client);
        }

        public IEnumerable<Appointment> GetAllAppointments()
        {
            return appointmentRepository.GetAllFutureAppointments();
        }

        public IEnumerable<Appointment> GetAppointmentsFutureMedicLastNameForClient(string medicLastName)
        {

            //var medic = medicRepository.GetMedicByLastName(medicId);
            return appointmentRepository.GetAppointmentsFutureMedicByLastNameForClient(medicLastName);
        }

        public void MakeAppointment(Appointment appointment, string clientId)
        {
            Guid clientIdGuid = Guid.Empty;
            if (!Guid.TryParse(clientId, out clientIdGuid))
            {
                throw new Exception("Invalid Guid Format");
            }
            var client = clientRepository.GetClientByUserId(clientIdGuid);
            if (client == null)
            {
                throw new EntityNotFoundException(clientIdGuid);
            }

            appointment.Client = client;
            appointmentRepository.Update(appointment);
        }

        public void DeleteAppointment(Appointment appointment, string clientId)
        {
            Guid clientIdGuid = Guid.Empty;
            if (!Guid.TryParse(clientId, out clientIdGuid))
            {
                throw new Exception("Invalid Guid Format");
            }
            var client = clientRepository.GetClientByUserId(clientIdGuid);
            if (client == null)
            {
                throw new EntityNotFoundException(clientIdGuid);
            }

            appointmentRepository.Delete(appointment);
        }

        public Appointment GetAppointmentById(Guid appointmentID)
        {

            var appointment = appointmentRepository.GetAppointmentByID(appointmentID);
            if (appointment == null)
            {
                throw new EntityNotFoundException(appointmentID);
            }

            return appointment;
        }

        public IEnumerable<Appointment> GetAppointmentsFutureClient(string clientId)
        {
            Guid clientIdGuid = Guid.Empty;
            if (!Guid.TryParse(clientId, out clientIdGuid))
            {
                throw new Exception("Invalid Guid Format");
            }
            var client = clientRepository.GetClientByUserId(clientIdGuid);
            if (client == null)
            {
                throw new EntityNotFoundException(clientIdGuid);
            }

            return appointmentRepository.GetAppointmentsFutureClient(clientIdGuid);
        }

        public IEnumerable<Appointment> GetAppointmentsPastClient(string clientId)
        {

            Guid clientIdGuid = Guid.Empty;
            if (!Guid.TryParse(clientId, out clientIdGuid))
            {
                throw new Exception("Invalid Guid Format");
            }
            var client = clientRepository.GetClientByUserId(clientIdGuid);
            if (client == null)
            {
                throw new EntityNotFoundException(clientIdGuid);
            }

            return appointmentRepository.GetAppointmentsPastClient(clientIdGuid);
        }
    }
}
