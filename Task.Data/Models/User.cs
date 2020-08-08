using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Task.Data.Enums;

namespace Task.Data.Models
{
    public  class User
    {
        [Required]
        public int UserID { get; set; }
        [Required]
        [StringLength(50)]
        public string FullName { get; set; }
        [Required]
        public DateTime Birthdate { get; set; }
        [Required]
        public Gender Gender { get; set; }
    }
}
