using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVGS.Models.ViewModel
{
    public class CheckoutCartViewModel
    {
        public IEnumerable<Games> MyCartItems { get; set; }
        public Dictionary<string, IEnumerable<Games>> FriendsCartItems { get; set; }

        public IEnumerable<CreditCards> CreditCards { get; set; }

        public IEnumerable<ShippingMailingAddresses> ShippingAddresses { get; set; }



    }
}
