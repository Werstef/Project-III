 using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using KristMedv2.ApplicationLogic.Services;
using KristMedv2.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;

namespace KristMedv2.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private  UserManager<ApplicationUser> _userManager;
        private  RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IHostingEnvironment _hostingEnvironment;
        //private readonly IEmailSender _emailSender;
        //private readonly AuthentificationService authentificationService;
        private readonly ClientsService clientsService;
        private readonly AdminsService adminsService;
        private readonly MedicsService medicsService;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            //AuthentificationService authentificationService,
            ClientsService clientsService,
            AdminsService adminsService,
            MedicsService medicsService,
            ILogger<RegisterModel> logger,
            IHostingEnvironment hostingEnvironment,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _hostingEnvironment = hostingEnvironment;
            _logger = logger;
            //_emailSender = emailSender;
            //this.authentificationService = authentificationService;
            this.clientsService = clientsService;
            this.adminsService = adminsService;
            this.medicsService = medicsService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {

            [Required]
            [Display(Name = "Username")]
            public string Username { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [Display(Name = "FirstName")]
            public string FirstName { get; set; }

            [Required]
            [Display(Name = "LastName")]
            public string LastName { get; set; }

            [Required]
            [Display(Name = "PhoneNo")]
            public string PhoneNo { get; set; }

            [Required]
            [Display(Name = "Role")]
            public string Role { get; set; }

            [Required]
            [Display(Name = "Profile Pic")]
            public IFormFile ProfilePicture { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (Input.ProfilePicture != null)
                {
                    string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + Input.ProfilePicture.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    Input.ProfilePicture.CopyTo(new FileStream(filePath, FileMode.Create));
                }

                var user = new ApplicationUser { UserName = Input.Username, Email = Input.Email, PhotoPath = uniqueFileName};
                var result = await _userManager.CreateAsync(user, Input.Password);
                //var user = authentificationService.CreateIdentityUser(Input.Email);
                //var result = authentificationService.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");


                    if (Input.Role == "Medic")
                    {
                        medicsService.CreateMedic(user.Id, Input.FirstName, Input.LastName);
                        ApplicationUser userMedic = await _userManager.FindByNameAsync(Input.Username);
                        await _userManager.AddToRoleAsync(userMedic, "Medic");
                    }
                    else if (Input.Role == "Admin")
                    {
                        adminsService.CreateAdmin(user.Id, Input.FirstName, Input.LastName);
                        ApplicationUser userAdmin = await _userManager.FindByNameAsync(Input.Username);
                        await _userManager.AddToRoleAsync(userAdmin, "Admin");
                    }
                    else
                    {
                        clientsService.CreateClient(user.Id, Input.FirstName, Input.LastName, Input.Email, Input.PhoneNo);
                        ApplicationUser userClient = await _userManager.FindByNameAsync(Input.Username);
                        await _userManager.AddToRoleAsync(userClient, "Client");
                    }

                        //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        //code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                        //var callbackUrl = Url.Page(
                        //    "/Account/ConfirmEmail",
                        //    pageHandler: null,
                        //    values: new { area = "Identity", userId = user.Id, code = code },
                        //    protocol: Request.Scheme);

                        //await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                        if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
