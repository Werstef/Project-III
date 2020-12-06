using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KristMedv2.ApplicationLogic.DataModels;
using KristMedv2.DataAccess;
using KristMedv2.ApplicationLogic.Services;
using Microsoft.AspNetCore.Identity;
using KristMedv2.Models;
using KristMedv2.Models.Admins;
using Microsoft.AspNetCore.Authorization;

namespace KristMedv2.Controllers
{
    public class AdminsController : Controller
    {
        private readonly AdminsService adminsService;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminsController(AdminsService adminsService, UserManager<ApplicationUser> userManager)
        {
            this.adminsService = adminsService;
            _userManager = userManager;
        }

        // GET: Admins
        public ActionResult Index()
        {
            try
            {
                return View();
            }
            catch (Exception)
            {
                return BadRequest("Invalid request received ");
            }
        }

        [Authorize(Roles ="Admin")]
        [HttpGet]
        public async Task<IActionResult> EditAccount()
        {
            var userId = _userManager.GetUserId(User);

            var user_identity = await _userManager.FindByIdAsync(userId);
            var user_email = await _userManager.GetEmailAsync(user_identity);
            var user_username = await _userManager.GetUserNameAsync(user_identity);

            var admin = adminsService.GetAdminById(userId);

            AdminEditAccountViewModel adminEditAccountViewModel = new AdminEditAccountViewModel
            {
                UserId = userId,
                FirstName = admin.FirstName,
                LastName = admin.LastName,
                Email = user_email,
                Username = user_username
            };
            return View(adminEditAccountViewModel);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> EditAccount([FromForm]AdminEditAccountViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user_identity = await _userManager.FindByIdAsync(model.UserId);
            user_identity.Email = model.Email;
            user_identity.UserName = model.Username;

            var result = await _userManager.UpdateAsync(user_identity);

            Guid adminIdGuid = Guid.Empty;
            if (!Guid.TryParse(model.UserId, out adminIdGuid))
            {
                throw new Exception("Invalid Guid Format");
            }


            adminsService.EditAccount(adminIdGuid, model.FirstName, model.LastName);
            return Redirect(Url.Action("Index", "Admins"));
        }

        [HttpGet]
        public IActionResult AddEquipment()
        {
            return View();
        }




        [HttpPost]
        public IActionResult AddEquipment([FromForm]AdminAddEquipmentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            //var userId = userManager.GetUserId(User);
            adminsService.AddEquipment(model.Name, model.Manufacturer, model.UsageTime);
            return Redirect(Url.Action("Equipment", "Admins"));

        }

        [HttpGet]
        public IActionResult Equipment()
        {
            try
            {

                var equipmentTypes = adminsService.GetEquipmentTypes();

                return View(new AdminEquipmentViewModel { EquipmentTypes = equipmentTypes });
            }
            catch (Exception)
            {
                return BadRequest("Invalid request received ");
            }
        }

        [HttpGet]
        public IActionResult AddMedicine()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddMedicine([FromForm]AdminAddMedicineViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            //var userId = userManager.GetUserId(User);
            adminsService.AddMedicine(model.Name, model.Manufacturer, model.QuantityStock, model.ExpirationDate);
            return Redirect(Url.Action("Medicine", "Admins"));

        }


        [HttpGet]
        public IActionResult Medicine()
        {
            try
            {

                var medicationTypes = adminsService.GetMedicineTypes();

                return View(new AdminMedicineViewModel { MedicationTypes = medicationTypes });
            }
            catch (Exception)
            {
                return BadRequest("Invalid request received ");
            }
        }


        [HttpGet]
        public IActionResult ContactMessages()
        {
            try
            {

                var contactMessages = adminsService.GetContactMessages();

                return View(new AdminContactMessagesViewModel { ContactMessages = contactMessages });
            }
            catch (Exception)
            {
                return BadRequest("Invalid request received ");
            }
        }

        [HttpGet]
        public IActionResult AddAppointment()
        {
            var medics = adminsService.GetMedics();
            AdminAddAppointmentViewModel adminAddAppointmentViewModel = new AdminAddAppointmentViewModel
            {
                Medics = new SelectList(medics, "Id", "LastName")
            };
            return View(adminAddAppointmentViewModel);
        }

        public IActionResult AddAppointment([FromForm]AdminAddAppointmentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var userId = _userManager.GetUserId(User);
            adminsService.AddAppointment(model.Medic, model.Duration, model.Date, userId);
            return Redirect(Url.Action("Appointment", "Admins"));

        }

        [HttpGet]
        public IActionResult Appointment()
        {
            try
            {

                var appointments = adminsService.GetAllAppointments();

                return View(new AdminAppointmentViewModel { Appointments = appointments });
            }
            catch (Exception)
            {
                return BadRequest("Invalid request received ");
            }
        }

    }
}
