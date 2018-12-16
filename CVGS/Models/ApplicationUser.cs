using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CVGS.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [Display(Name = "User Name")]
        public override string UserName { get => base.UserName; set => base.UserName = value; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Display(Name = "Date of Birth")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Receive Promotional Emails")]
        public bool isReceivePromotionalEmails { get; set; }

        public string Role { get; set; }

        public string PreferedGamingPlateForm { get; set; }

        public string PreferedGamingGenre { get; set; }

        [NotMapped]
        public bool isEmployee { get; set; }
        [NotMapped]
        public bool isSuperAdmin { get; set; }
    }
}
