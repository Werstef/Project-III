using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KristMedv2.ApplicationLogic.Services;
using KristMedv2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace KristMedv2.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LogoutModel> _logger;
        //private readonly AuthentificationService authentificationService;

        public LogoutModel(SignInManager<ApplicationUser> signInManager, ILogger<LogoutModel> logger)
            //AuthentificationService authentificationService)
        {
            _signInManager = signInManager;
            _logger = logger;
            //this.authentificationService = authentificationService;
        }

        public void OnGet()
        {
            _signInManager.SignOutAsync().GetAwaiter().GetResult();
            _logger.LogInformation("User logged out.");
            
           //RedirectToPage();
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToPage();
            }
        }
    }
}
