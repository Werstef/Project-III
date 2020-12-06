using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KristMedv2.ApplicationLogic.DataModels
{
    public class ContactMessage
    {

        [Key]
        public Guid Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [Column(TypeName = "varchar(5000)")]
        public string MessageText { get; set; }

        public Client Client { get; set; }
    }
}