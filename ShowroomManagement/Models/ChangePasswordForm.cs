using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShowroomManagement.Models
{
    public class ChangePasswordForm
    {
        public string oldPasswprd { get; set; }
        public string newPasswprd { get; set; }
        public string confirmPassword { get; set; }
    }
}