using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCoreAPI.ViewModels
{
    public class RegistrationViewModel
    {
      

        [Required(ErrorMessage = "Email is mandatory")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is mandatory")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is mandatory")]
        [Compare("Password",ErrorMessage ="Password & Confirm Password must match.")]
        public string ConfirmPassword { get; set; }

    }
}
