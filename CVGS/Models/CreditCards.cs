using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CVGS.Models
{
    public class CreditCards
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string ApplicationUserId { get; set; }

        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        [MinLength(16, ErrorMessage = "Invalid Credit Card Number")]
        [Display(Name = "Credit Card Number")]
        public string CreditCardNumberHash { get; set; }

        public string Last4Digits { get; set; }
        public string ExpirationMonth { get; set; }
        public string ExpirationYear { get; set; }
        [MinLength(3, ErrorMessage = "Invalid CVV Number")]
        [Display(Name = "CVV Number")]
        public string CVVHash { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
    }
}
