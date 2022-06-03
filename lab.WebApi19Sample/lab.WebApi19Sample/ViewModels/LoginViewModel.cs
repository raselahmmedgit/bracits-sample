using System.ComponentModel.DataAnnotations;

namespace lab.WebApi19Sample.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Pin Number")]
        public bool PinNumber { get; set; }

        [Required]
        public string LoginType { get; set; }
    }
}
