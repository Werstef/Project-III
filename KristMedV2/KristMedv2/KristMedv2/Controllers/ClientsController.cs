using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KristMedv2.ApplicationLogic.DataModels;
using KristMedv2.DataAccess;
using Microsoft.AspNetCore.Identity;
using KristMedv2.ApplicationLogic.Services;
using KristMedv2.Models.Clients;
using Microsoft.AspNetCore.Authorization;
using KristMedv2.Models;

namespace KristMedv2.Controllers
{
    [Authorize]
    public class ClientsController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ClientsService clientsService;
        private readonly AdminsService adminsService;
        private readonly MedicsService medicsService;

        public ClientsController(UserManager<ApplicationUser> userManager, ClientsService clientsService,
            AdminsService adminsService, MedicsService medicsService)
        {
            _userManager = userManager;
            this.clientsService = clientsService;
            this.adminsService = adminsService;
            this.medicsService = medicsService;
        }

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

        [HttpGet]
        public IActionResult ContactMessages()
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                var client = clientsService.GetClientByUserId(userId);
                var contactMessages = clientsService.GetContactMessagesForClient(userId);

                return View(new ClientContactMessagesViewModel { ContactMessages = contactMessages });
            }
            catch (Exception)
            {
                return BadRequest("Invalid request received ");
            }
        }

        [HttpGet]
        public IActionResult AddContactMessage()
        {
            return View();
        }


        [HttpPost]
        public IActionResult AddContactMessage([FromForm]ClientAddContactMessageViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var userId = _userManager.GetUserId(User);
            clientsService.AddContactMessage(userId, model.FullName, model.Email, model.Description);
            return Redirect(Url.Action("Index", "Clients"));

        }


        [Authorize(Roles = "Client")]
        [HttpGet]
        public async Task<IActionResult> EditAccount()
        {
            var userId = _userManager.GetUserId(User);

            var user_identity = await _userManager.FindByIdAsync(userId);
            var user_email = await _userManager.GetEmailAsync(user_identity);
            var user_username = await _userManager.GetUserNameAsync(user_identity);

            var client = clientsService.GetClientById(userId);

            ClientEditAccountViewModel clientEditAccountViewModel = new ClientEditAccountViewModel
            {
                UserId = userId,
                FirstName = client.FirstName,
                LastName = client.LastName,
                PhoneNo = client.PhoneNo,
                Email = user_email,
                Username = user_username
            };
            return View(clientEditAccountViewModel);
        }

        [Authorize(Roles = "Client")]
        [HttpPost]
        public async Task<IActionResult> EditAccount([FromForm]ClientEditAccountViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user_identity = await _userManager.FindByIdAsync(model.UserId);
            user_identity.Email = model.Email;
            user_identity.UserName = model.Username;

            var result = await _userManager.UpdateAsync(user_identity);

            Guid clientIdGuid = Guid.Empty;
            if (!Guid.TryParse(model.UserId, out clientIdGuid))
            {
                throw new Exception("Invalid Guid Format");
            }


            clientsService.EditAccount(clientIdGuid, model.FirstName, model.LastName,
                                         model.PhoneNo);
            return Redirect(Url.Action("Index", "Admins"));
        }

        [HttpGet]
        public IActionResult SearchAppointments(string searchBy, string search)
        {
            switch (searchBy)
            {
                case "Medic":
                    return View(new ClientSearchAppointmentsViewModel { Appointments = clientsService.GetAppointmentsFutureMedicLastNameForClient(search) });
                case "NoFilter":
                    var appointments = clientsService.GetAllAppointments();
                    return View(new ClientSearchAppointmentsViewModel { Appointments = appointments });
                default:
                    var appointments_default = clientsService.GetAllAppointments();
                    return View(new ClientSearchAppointmentsViewModel { Appointments = appointments_default });
            }
        }

        
        public IActionResult MakeAppointment(Guid Id)
        {
            var appointment = clientsService.GetAppointmentById(Id);
            if (appointment == null)
            {
                return BadRequest("Appointment not found");
            }
            else
            {
                var userId = _userManager.GetUserId(User);
                clientsService.MakeAppointment(appointment, userId);
                return Redirect(Url.Action("FutureAppointments", "Clients"));

            }

        }

        [HttpGet]
        public IActionResult FutureAppointments()
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                var future_appointments = clientsService.GetAppointmentsFutureClient(userId);

                return View(new ClientFutureAppointmentsViewModel { FutureAppointments = future_appointments });
            }
            catch (Exception)
            {
                return BadRequest("Invalid request received ");
            }
        }

        public IActionResult DeleteAppointment(Guid Id)
        {
            var appointment = clientsService.GetAppointmentById(Id);
            if (appointment == null)
            {
                return BadRequest("Flight not found");
            }
            else
            {
                var userId = _userManager.GetUserId(User);
                clientsService.DeleteAppointment(appointment, userId);
                return Redirect(Url.Action("FutureAppointments", "Clients"));

            }

        }

        [HttpGet]
        public IActionResult PastAppointments()
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                var past_appointments = clientsService.GetAppointmentsPastClient(userId);

                return View(new ClientPastAppointmentsViewModel { PastAppointments = past_appointments });
            }
            catch (Exception)
            {
                return BadRequest("Invalid request received ");
            }
        }
    }
}
