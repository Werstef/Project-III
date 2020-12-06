using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KristMedv2.ApplicationLogic.DataModels
{
    public class Medic
    {
        [Key]
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string? Code { get; set; }


    }
}