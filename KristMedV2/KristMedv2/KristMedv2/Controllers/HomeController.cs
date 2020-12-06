using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using KristMedv2.Models;
using Microsoft.AspNetCore.Identity;
using KristMedv2.Data;
using System.IO;

namespace KristMedv2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> userManager;

        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            this.userManager = userManager;
        }

        public async Task<ActionResult> UserPhotosAsync()
        {

            var userId = userManager.GetUserId(User);
            var user_identity = await userManager.FindByIdAsync(userId);

            var userPhoto = "~/Images/" + (user_identity.PhotoPath ?? "noimage.jpg");

            return base.File(userPhoto, "image/jpg");       
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
