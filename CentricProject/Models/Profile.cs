using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace CentricProject.Models
{
    public class Profile
    {
        // Using guid for a unique ID
        public System.Guid id { get; set; }

        // Data Annotations for Employee First Name
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name is required!")]
        [StringLength(25)]
        public string firstName { get; set; }

        // Data Annotations for Employee Last Name
        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last Name is required!")]
        [StringLength(25)]
        public string lastName { get; set; }

        // Data Annotations for Employee Preffered Name
        [Display(Name = "Preffered Name")]
        [Required(ErrorMessage = "Preffered Name is required!")]
        [StringLength(25)]
        public string prefferedName { get; set; }

        // Data Annotations for Employee Phone Number
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^(\(\d{3}\) |\d{3}-)\d{3}-\d{4}$", ErrorMessage = "Phone Number must be in xxx-xxx-xxxx or (xxx)-xxx-xxxx format!")]
        [Required]
        public string phoneNumber { get; set; }

        // Data Annotations for Employee Email
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email is required!")]
        [StringLength(50)]
        public string email { get; set; }

        // Data Annotations for Employee Hire Date
        [Display(Name = "Stared Working")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Hire Date is required! -> MM/DD/YYYY")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyy}", ApplyFormatInEditMode = true)]
        public DateTime hireDate { get; set; }

        // Data Annotations for Employee Business Unit
        [Display(Name = "Business Unit")]
        [Required(ErrorMessage = "Business Unit is required!")]
        [StringLength(50)]
        public string businessUnit { get; set; }

        // Data Annotations for Employee Position
        [Display(Name = "Position")]
        [Required(ErrorMessage = "Position is required!")]
        [StringLength(50)]
        public string position { get; set; }
    }
}