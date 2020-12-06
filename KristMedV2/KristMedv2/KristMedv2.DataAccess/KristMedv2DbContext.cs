using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KristMedv2.ApplicationLogic.DataModels;
using Microsoft.EntityFrameworkCore;


namespace KristMedv2.DataAccess
{
    public class KristMedv2DbContext : DbContext
    {
        public KristMedv2DbContext(DbContextOptions<KristMedv2DbContext> options) : base(options)
        {

        }

        public DbSet<Admin> Admins { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<Medic> Medics { get; set; }

        public DbSet<ContactMessage> ContactMessages { get; set; }

        public DbSet<Appointment> Appointments { get; set; }

        public DbSet<Treatment> Treatments { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Medication_Treatment> Medication_Treatments { get; set; }

        public DbSet<MedicationType> MedicationTypes { get; set; }

        public DbSet<Equipment_Treatment> Equipment_Treatments { get; set; }

        public DbSet<EquipmentType> EquipmentTypes { get; set; }




    }
}
