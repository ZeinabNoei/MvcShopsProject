using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace DataLayer.ViewModels
{
   public  class RegisterViewModels
    {
        [Display(Name = "UserName")]
        [Required(ErrorMessage = "Please Enter {0}")]
        public string UserName { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Please Enter {0}")]
        [RegularExpression(@"^\S.+\@\S.+\.\S[a-z]{1,5}", ErrorMessage = "please Enter Correct Email")]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please Enter {0}")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "RePassword")]
        [Required(ErrorMessage = "Please Enter {0}")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The passwords are not the same")]
        public string RePassword { get; set; }
  
    }
    public class LoginViewModel {
        [Display(Name ="Email")]
        [Required(ErrorMessage = "Please Enter Email")]
        [EmailAddress(ErrorMessage = "Please Enter Email")]
        public string  Email { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage ="Please Enter full")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "RememberMe")]
        public bool RememberMe { get; set; }


    }
    public class ForgotPasswordViewModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Please Enter Email")]
        [EmailAddress(ErrorMessage = "Please Enter Email")]
        public string Email { get; set; }
    }
    public class RecoveryPasswordViewModel
    {
        [Display(Name = "New Password")]
        [Required(ErrorMessage = "Please Enter {0}")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "New RePassword")]
        [Required(ErrorMessage = "Please Enter {0}")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The passwords are not the same")]
        public string RePassword { get; set; }

    }
    public class ChangePasswordViewModel
    {
        [Display(Name = "Old Password")]
        [Required(ErrorMessage = "Please Enter {0}")]
        [DataType(DataType.Password)]
        public string  OldPassword { get; set; }

        [Display(Name = "New Password")]
        [Required(ErrorMessage = "Please Enter {0}")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "New RePassword")]
        [Required(ErrorMessage = "Please Enter {0}")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The passwords are not the same")]
        public string RePassword { get; set; }
    }
}
