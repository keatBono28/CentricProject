using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CentricProject.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public virtual ProfileDetails ProfileDetails { get; set; }
        
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ProfileDetails
    {
        public int id { get; set; }
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
        // Data Annotation for the Profile Image
        [Display(Name = "ProfilePicture")]
        
        public byte[] profileImage { get; set; }
    }
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base("DefaultConnection")
        {
        }
        public DbSet<ProfileDetails> ProfileDetails { get; set; }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}