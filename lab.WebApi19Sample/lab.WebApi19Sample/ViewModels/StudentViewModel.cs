using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace lab.WebApi19Sample.ViewModels
{
    public class StudentViewModel
    {
        [Key]
        public string StudentId { get; set; }

        [DisplayName("Name")]
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(200)]
        public string StudentName { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "Email is required")]
        [MaxLength(200)]
        public string EmailAddress { get; set; }

        [DisplayName("Date Of Birth")]
        [Required(ErrorMessage = "Date Of Birth is required")]
        public DateTime DateOfBirth { get; set; }
    }
}
