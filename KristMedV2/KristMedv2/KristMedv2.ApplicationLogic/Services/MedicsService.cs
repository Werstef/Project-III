using KristMedv2.ApplicationLogic.Abstractions;
using KristMedv2.ApplicationLogic.DataModels;
using KristMedv2.ApplicationLogic.Exceptions;
using System;
using System.Collections.Generic;

namespace KristMedv2.ApplicationLogic.Services
{
    public class MedicsService
    {
        IMedicRepository medicRepository;
        IAppointmentRepository appointmentRepository;
        IEquipmentTypeRepository equipmentTypeRepository;
        IMedicationTypeRepository medicationTypeRepository;
        IClientRepository clientRepository;
        ITreatmentRepository treatmentRepository;
        IMedication_TreatmentRepository medication_TreatmentRepository;
        IEquipment_TreatmentRepository equipment_TreatmentRepository;

        public MedicsService(IMedicRepository medicRepository, 
            IAppointmentRepository appointmentRepository, IEquipmentTypeRepository equipmentTypeRepository, 
            IMedicationTypeRepository medicationTypeRepository, IClientRepository clientRepository, ITreatmentRepository treatmentRepository, IMedication_TreatmentRepository medication_TreatmentRepository, 
            IEquipment_TreatmentRepository equipment_TreatmentRepository)
        {
            this.medicRepository = medicRepository;
            this.appointmentRepository = appointmentRepository;
            this.equipmentTypeRepository = equipmentTypeRepository;
            this.medicationTypeRepository = medicationTypeRepository;
            this.clientRepository = clientRepository;
            this.treatmentRepository = treatmentRepository;
            this.medication_TreatmentRepository = medication_TreatmentRepository;
            this.equipment_TreatmentRepository = equipment_TreatmentRepository;
        }

        public Medic CreateMedic(string userId, string firstName, string lastName)
        {
            var medic = new Medic
            {
                UserId = Guid.Parse(userId),
                FirstName = firstName,
                LastName = lastName
            };
            medicRepository.Add(medic);
            return medic;
        }

        public IEnumerable<Appointment> GetAppointmentsFutureMedic(string medicId)
        {
            Guid medicIdGuid = Guid.Empty;
            if (!Guid.TryParse(medicId, out medicIdGuid))
            {
                throw new Exception("Invalid Guid Format");
            }
            var medic = medicRepository.GetMedicByUserId(medicIdGuid);
            if (medic == null)
            {
                throw new EntityNotFoundException(medicIdGuid);
            }

            return appointmentRepository.GetAppointmentsFutureMedic(medicIdGuid);
        }

        public IEnumerable<Appointment> GetAppointmentsPastMedic(string medicId)
        {

            Guid medicIdGuid = Guid.Empty;
            if (!Guid.TryParse(medicId, out medicIdGuid))
            {
                throw new Exception("Invalid Guid Format");
            }
            var medic = medicRepository.GetMedicByUserId(medicIdGuid);
            if (medic == null)
            {
                throw new EntityNotFoundException(medicIdGuid);
            }

            return appointmentRepository.GetAppointmentsPastMedic(medicIdGuid);
        }

        public IEnumerable<Appointment> GetAppointmentsTreatmentMedic(string medicId)
        {

            Guid medicIdGuid = Guid.Empty;
            if (!Guid.TryParse(medicId, out medicIdGuid))
            {
                throw new Exception("Invalid Guid Format");
            }
            var medic = medicRepository.GetMedicByUserId(medicIdGuid);
            if (medic == null)
            {
                throw new EntityNotFoundException(medicIdGuid);
            }

            return appointmentRepository.GetAppointmentsTreatmentMedic(medicIdGuid);
        }

        public void AddTreatment(Guid appointmentId, Guid clientId, Guid medicId, Guid equipmentId, 
            Guid medicationId, int equipmentUsageTime, int medicationQuantityUsed, string diagnosis
                    )
        {
            var appointment = appointmentRepository.GetAppointmentByID(appointmentId);
            var client = clientRepository.GetById(clientId);
            var medic = medicRepository.GetById(medicId);
            var equipment = equipmentTypeRepository.GetEquipmentById(equipmentId);
            var medication = medicationTypeRepository.GetMedicationById(medicationId);



            var treatment = treatmentRepository.Add(new Treatment()
            {
                Id = Guid.NewGuid(),
                Medic = medic,
                Client = client,
                Diagnosis = diagnosis,
                Equipments_Treatment = new List<Equipment_Treatment>(),
                Medications_Treatment = new List<Medication_Treatment>()
            });

            var equipment_treatment = equipment_TreatmentRepository.Add(new Equipment_Treatment
            {
                Id = Guid.NewGuid(),
                UsageTime = equipmentUsageTime,
            });

            var medication_treatment = medication_TreatmentRepository.Add(new Medication_Treatment
            {
                Id = Guid.NewGuid(),
                QuantityUsed = medicationQuantityUsed
            });

            equipment.Equipment_Treatments.Add(equipment_treatment);
            equipmentTypeRepository.Update(equipment);
            medication.Medication_Treatments.Add(medication_treatment);
            medicationTypeRepository.Update(medication);
            treatment.Medications_Treatment.Add(medication_treatment);
            treatment.Equipments_Treatment.Add(equipment_treatment);
            treatmentRepository.Update(treatment);
            appointment.Treatment = treatment;
            appointmentRepository.Update(appointment);
        }

        

        public Treatment GetTreatmentById(Guid id)
        {
            var treatment = treatmentRepository.GetTreatmentByID(id);
            if (treatment == null)
            {
                throw new EntityNotFoundException(id);
            }

            return treatment;
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
        public IEnumerable<EquipmentType> GetEquipments()
        {
            return equipmentTypeRepository.GetAll();
        }

        public IEnumerable<MedicationType> GetMedications()
        {
            return medicationTypeRepository.GetAll();
        }

        
    }
}
