using KristMedv2.ApplicationLogic.Abstractions;
using KristMedv2.ApplicationLogic.DataModels;
using KristMedv2.ApplicationLogic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KristMedv2.ApplicationLogic.Services
{
    public class AdminsService
    {
        IAdminRepository adminRepository;
        IEquipmentTypeRepository equipmentTypeRepository;
        IContactMessageRepository contactMessageRepository;
        IMedicationTypeRepository medicationTypeRepository;
        IMedicRepository medicRepository;
        IAppointmentRepository appointmentRepository;

        public AdminsService(IAdminRepository adminRepository, IEquipmentTypeRepository equipmentTypeRepository, IContactMessageRepository contactMessageRepository, IMedicationTypeRepository medicationTypeRepository, 
            IMedicRepository medicRepository, IAppointmentRepository appointmentRepository)
        {
            this.adminRepository = adminRepository;
            this.equipmentTypeRepository = equipmentTypeRepository;
            this.contactMessageRepository = contactMessageRepository;
            this.medicationTypeRepository = medicationTypeRepository;
            this.medicRepository = medicRepository;
            this.appointmentRepository = appointmentRepository;
        }

        public Admin CreateAdmin(string userId, string firstName, string lastName)
        {
            var admin = new Admin
            {
                UserId = Guid.Parse(userId),
                FirstName = firstName,
                LastName = lastName
            };
            adminRepository.Add(admin);
            return admin;
        }

        public Admin GetAdminById(string adminId)
        {
            Guid adminIdGuid = Guid.Empty;
            if (!Guid.TryParse(adminId, out adminIdGuid))
            {
                throw new Exception("Invalid Guid Format");
            }

            var admin = adminRepository.GetAdminByUserId(adminIdGuid);
            if (admin == null)
            {
                throw new EntityNotFoundException(adminIdGuid);
            }

            return admin;

        }

        public void EditAccount(Guid userId, string firstName, string lastName)
        {

            var admin = adminRepository.GetAdminByUserId(userId);

            admin.FirstName = firstName;
            admin.LastName = lastName;

            adminRepository.Update(admin);
        }

        public void AddEquipment(string name, string manufacturer, int usageTime)
        {
            equipmentTypeRepository.Add(new EquipmentType()
            {
                Id = Guid.NewGuid(),
                Name = name,
                Manufacturer = manufacturer,
                UsageTimeAvailable = usageTime,
                Equipment_Treatments = new List<Equipment_Treatment>(),
            });
        }

        public IEnumerable<EquipmentType> GetEquipmentTypes()
        {
            return equipmentTypeRepository.GetAll()
                                    .AsEnumerable();
        }

        public void AddMedicine(string name, string manufacturer, int quantityStock, DateTime expirationDate)
        {
            /*Guid userIdGuid = Guid.Empty;
            if (!Guid.TryParse(userId, out userIdGuid))
            {
                throw new Exception("Invalid Guid Format");
            }
            var client = clientRepository.GetClientByUserId(userIdGuid);
            if (client == null)
            {
                throw new EntityNotFoundException(userIdGuid);
            }*/

            medicationTypeRepository.Add(new MedicationType()
            {
                Id = Guid.NewGuid(),
                Name = name,
                Manufacturer = manufacturer,
                QuantityStock = quantityStock,
                ExpirationDate = expirationDate,
                Medication_Treatments = new List<Medication_Treatment>()
            });
        }

        public IEnumerable<MedicationType> GetMedicineTypes()
        {
            return medicationTypeRepository.GetAll()
                            .AsEnumerable();
        }

        public IEnumerable<ContactMessage> GetContactMessages()
        {
            return contactMessageRepository.GetAllContactMessages();
        }

        public IEnumerable<Medic> GetMedics()
        {
            return medicRepository.GetAllMedics();
        }

        public IEnumerable<Appointment> GetAllAppointments()
        {
            return appointmentRepository.GetAllAppointments();
        }

        public void AddAppointment(Guid medicId, int duration, DateTime date, string adminId)
        {

            Guid adminIdGuid = Guid.Empty;
            if (!Guid.TryParse(adminId, out adminIdGuid))
            {
                throw new Exception("Invalid Guid Format");
            }
            var admin = adminRepository.GetAdminByUserId(adminIdGuid);
            if (admin == null)
            {
                throw new EntityNotFoundException(adminIdGuid);
            }

            var medic = medicRepository.GetMedicByID(medicId);

            var appointment = appointmentRepository.Add(new Appointment()
            {
                Id = Guid.NewGuid(),
                Duration = duration,
                Date = date,
                Admin = admin,
                Medic = medic
            });
        }
    }
}
