using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KristMedv2.ApplicationLogic.DataModels
{
    public class Post
    {

        [Key]
        public Guid Id { get; set; }

        [Column(TypeName = "varchar(5000)")]
        public string Description { get; set; }

    }
}