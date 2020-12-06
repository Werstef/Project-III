using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KristMedv2.ApplicationLogic.DataModels;
using KristMedv2.ApplicationLogic.Services;
using KristMedv2.Models;
using KristMedv2.Models.Medics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KristMedv2.Controllers
{
    public class MedicsController : Controller
    {

        private readonly MedicsService medicsService;
        private readonly UserManager<ApplicationUser> _userManager;

        public MedicsController(MedicsService medicsService, UserManager<ApplicationUser> userManager)
        {
            this.medicsService = medicsService;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult FutureAppointments()
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                var future_appointments = medicsService.GetAppointmentsFutureMedic(userId);

                return View(new MedicFutureAppointmentsViewModel { FutureAppointments = future_appointments });
            }
            catch (Exception)
            {
                return BadRequest("Invalid request received ");
            }
        }

        [HttpGet]
        public IActionResult PastAppointments()
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                var past_appointments = medicsService.GetAppointmentsPastMedic(userId);

                return View(new MedicPastAppointmentsViewModel { PastAppointments = past_appointments });
            }
            catch (Exception)
            {
                return BadRequest("Invalid request received ");
            }
        }

        [HttpGet]
        public IActionResult AddTreatment(Guid Id)
        {
            var appointment = medicsService.GetAppointmentById(Id);
            var equipments = medicsService.GetEquipments();
            var medications = medicsService.GetMedications();
            MedicAddTreatmentViewModel medicAddTreatmentViewModel = new MedicAddTreatmentViewModel
            {
                AppointmentId = appointment.Id,
                ClientId = appointment.Client.Id,
                MedicId = appointment.Medic.Id,
                Equipments = new SelectList(equipments, "Id", "Name"),
                Medications = new SelectList(medications, "Id", "Name")
            };
            return View(medicAddTreatmentViewModel);
        }
        [HttpPost]
        public IActionResult AddTreatment([FromForm]MedicAddTreatmentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            medicsService.AddTreatment(model.AppointmentId, model.ClientId, model.MedicId,
                                         model.Equipment, model.Medication, model.EquipmentUsageTime, 
                                         model.MedicationQuantityUsed, model.Diagnosis
                                        );
            return Redirect(Url.Action("AppointmentsTreatment", "Medics"));
        }


        [HttpGet]
        public IActionResult AppointmentsTreatment()
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                var appointments_treatments = medicsService.GetAppointmentsTreatmentMedic(userId);

                return View(new MedicAppointmentsTreatmentViewModel { AppointmentsTreatment = appointments_treatments });
            }
            catch (Exception)
            {
                return BadRequest("Invalid request received ");
            }
        }

       /* [HttpGet]
        public IActionResult TreatmentDetails(Guid Id)
        {
            var treatment = medicsService.GetTreatmentById(Id);
            
            MedicTreatmentDetailsViewModel medicTreatmentDetailsViewModel = new MedicTreatmentDetailsViewModel
            {
                Treatment = treatment
            };
            return View(medicTreatmentDetailsViewModel);
        }*/
    }
}