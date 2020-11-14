using System;
using System.ComponentModel.DataAnnotations;

namespace Lab23.Data.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string Hash { get; set; }
        public DateTime LastLoggedInDate { get; set; }
    }
}