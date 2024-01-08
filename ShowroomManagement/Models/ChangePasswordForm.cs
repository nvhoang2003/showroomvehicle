using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShowroomManagement.Models
{
    public class ChangePasswordForm
    {
        [DisplayName("Old Password")]
        public string oldPasswprd { get; set; }
        [DisplayName("New Password")]
        public string newPasswprd { get; set; }
        [DisplayName("Confirm Password")]
        public string confirmPassword { get; set; }
    }
}