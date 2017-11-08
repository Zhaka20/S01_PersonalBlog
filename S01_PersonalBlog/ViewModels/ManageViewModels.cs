using S01_PersonalBlog.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.ComponentModel.DataAnnotations.Schema;

namespace S01_PersonalBlog.ViewModels
{
    public class UserProfileViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Nickname")]
        [MinLength(2, ErrorMessage = "Nickname should be at least 2 characters long")]
        [MaxLength(15, ErrorMessage = "Nickname should be less than 16 characters")]
        public string NickName { get; set; }

        [Display(Name = "Last name")]
        [MinLength(2, ErrorMessage = "Last name should be at least 2 characters long")]
        [MaxLength(36, ErrorMessage = "Last name should be less than 36 characters")]
        public string LastName { get; set; }

        [Display(Name = "First name")]
        [MinLength(2, ErrorMessage = "First name should be at least 2 characters long")]
        [MaxLength(36, ErrorMessage = "First name should be less than 36 characters")]
        public string FirstName { get; set; }

        [MinLength(200, ErrorMessage = "About should be at least 200 characters long")]
        [MaxLength(800, ErrorMessage = "About should be less than 800 characters")]
        [DataType(DataType.MultilineText)]   
        public string About { get; set; }

        [StringLength(255)]
        [Display(Name = "Avatar")]
        public string ImagePath { get; set; }

    }
    public class IndexViewModel
    {
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Nickname")]
        public string NickName { get; set; }

        [Display(Name ="Last name")]
        public string LastName { get; set; }

        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Display(Name = "Blogger ID")]
        public int BloggerId { get; set; }

        public string About { get; set; }
        [Display(Name ="Avatar")]
        public string ImagePath { get; set; }
    }

    public class ManageLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; }
        public IList<AuthenticationDescription> OtherLogins { get; set; }
    }

    public class FactorViewModel
    {
        public string Purpose { get; set; }
    }

    public class SetPasswordViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string Number { get; set; }
    }

    public class VerifyPhoneNumberViewModel
    {
        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }

    public class ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
    }
}