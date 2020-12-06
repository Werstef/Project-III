using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using KristMedv2.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using KristMedv2.DataAccess;
using KristMedv2.ApplicationLogic.Abstractions;
using KristMedv2.ApplicationLogic.Services;
using KristMedv2.Models;

namespace KristMedv2
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<KristMedv2DbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            /*services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();*/

            /* //Password Strength Setting
             services.Configure<IdentityOptions>(options =>
             {
                 // Password settings
                 options.Password.RequireDigit = true;
                 options.Password.RequiredLength = 8;
                 options.Password.RequireNonAlphanumeric = false;
                 options.Password.RequireUppercase = false;
                 options.Password.RequireLowercase = false;
                 options.Password.RequiredUniqueChars = 6;

                 // Lockout settings
                 options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                 options.Lockout.MaxFailedAccessAttempts = 10;
                 options.Lockout.AllowedForNewUsers = true;

                 // User settings
                 options.User.RequireUniqueEmail = true;
             });

             //Setting the Account Login page
             services.ConfigureApplicationCookie(options =>
             {
                 // Cookie settings
                 options.Cookie.HttpOnly = true;
                 options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                 options.LoginPath = "/Account/Login"; // If the LoginPath is not set here,
                                                       // ASP.NET Core will default to /Account/Login
                 options.LogoutPath = "/Account/Logout"; // If the LogoutPath is not set here,
                                                         // ASP.NET Core will default to /Account/Logout
                 options.AccessDeniedPath = "/Account/AccessDenied"; // If the AccessDeniedPath is
                                                                     // not set here, ASP.NET Core 
                                                                     // will default to 
                                                                     // /Account/AccessDenied
                 options.SlidingExpiration = true;
             });*/

            services.AddTransient<IAdminRepository, AdminRepository>();
            services.AddTransient<IClientRepository, ClientRepository>();
            services.AddTransient<IMedicRepository, MedicRepository>();
            services.AddTransient<IContactMessageRepository, ContactMessageRepository>();
            services.AddTransient<IMedicationTypeRepository, MedicationTypeRepository>();
            services.AddTransient<IEquipmentTypeRepository, EquipmentTypeRepository>();
            services.AddTransient<IAppointmentRepository, AppointmentRepository>();
            services.AddTransient<ITreatmentRepository, TreatmentRepository>();
            services.AddTransient<IEquipment_TreatmentRepository, Equipment_TreatmentRepository>();
            services.AddTransient<IMedication_TreatmentRepository, Medication_TreatmentRepository>();


            services.AddScoped<ClientsService>();
            services.AddScoped<AdminsService>();
            services.AddScoped<MedicsService>();
            //services.AddScoped<AuthentificationService>();

            services.AddControllersWithViews();
            services.AddRazorPages();
                            

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider services)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
            //CreateUserRoles(services).Wait();
        }

        private async Task CreateUserRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            IdentityResult roleResult;
            //Adding Admin Role
            var roleCheck = await RoleManager.RoleExistsAsync("Client");
            if (!roleCheck)
            {
                //create the roles and seed them to the database
                roleResult = await RoleManager.CreateAsync(new IdentityRole("Client"));
            }

            var roleCheck1 = await RoleManager.RoleExistsAsync("Admin");
            if (!roleCheck1)
            {
                //create the roles and seed them to the database
                roleResult = await RoleManager.CreateAsync(new IdentityRole("Admin"));
            }

            //Assign Admin role to the main User here we have given our newly registered 
            //login id for Admin management
           // IdentityUser user1 = await UserManager.FindByEmailAsync("syedshanumcain@gmail.com");
            //var User = new IdentityUser();
           // await UserManager.AddToRoleAsync(user1, "Admin");
        }
    }
}
