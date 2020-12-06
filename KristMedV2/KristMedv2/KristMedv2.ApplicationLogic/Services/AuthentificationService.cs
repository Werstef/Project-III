/*using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KristMedv2.ApplicationLogic.Services
{
    public class AuthentificationService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private UserManager<ApplicationUser> _userManager;
        private readonly ILogger<AuthentificationService> _logger;

        public AuthentificationService(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, 
                                ILogger<AuthentificationService> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }

        // Register Page
        public IdentityUser CreateIdentityUser(string email)
        {
            var user = new IdentityUser { UserName = email, Email = email };
            return user;
        }

        public async Task<IdentityResult> CreateAsync(IdentityUser user, string Password)
        {
            var result = await _userManager.CreateAsync(user, Password);
            return result;
        }

        public async Task<IdentityUser> FindByEmailAsync(string email)
        {
            IdentityUser userMedic = await _userManager.FindByEmailAsync( email);
            return userMedic;
        }

        public async Task AddToRoleAsync(IdentityUser user, string Role)
        {
            await _userManager.AddToRoleAsync(user, Role);
        }
        // Login Page

        public async Task<SignInResult> PasswordSingInAsync(string email, string Password, bool rememberMe)
        {
            var result = await _signInManager.PasswordSignInAsync(email, Password, rememberMe, lockoutOnFailure: false);
            return result;
        }

        // Logout Page

        public void SingOutGetResult()
        {
            _signInManager.SignOutAsync().GetAwaiter().GetResult();
            //poate pot sa folosesc logger si aici idk, de testat
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}*/
