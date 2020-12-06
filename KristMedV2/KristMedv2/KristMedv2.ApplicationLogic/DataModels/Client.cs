using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KristMedv2.ApplicationLogic.DataModels
{
    public class Client
    {
        [Key]
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PhoneNo { get; set; }


    }

}
