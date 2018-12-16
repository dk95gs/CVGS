using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CVGS.Models
{
    public class Orders
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }

        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        public string MyCartItems { get; set; }

        public string FriendsCartItems { get; set; }

        [Required]
        public double CartTotal { get; set; }

        [Required]
        public double TaxTotal { get; set; }

        [Required]
        public double CartPlusTaxTotal { get; set; }

        [Required]
        public string CreditCardHash { get; set; }
        [Required]

        public DateTime CreationDate { get; set; }


        public string ShipmentAddress { get; set; }

    }
}
