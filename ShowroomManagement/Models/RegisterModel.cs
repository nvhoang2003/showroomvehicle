using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShowroomManagement.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Please fill in the information")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "The username must have at least 6 characters")]
        public string username { get; set; }

        [Required(ErrorMessage = "Please fill in the information")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "The password must contain at least 8 characters")]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [Required(ErrorMessage = "Please re-enter the password")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        [DataType(DataType.Password)]
        public string confirmPassword { get; set; }
    }
}