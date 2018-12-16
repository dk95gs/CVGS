using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVGS.Models.ViewModel
{
    public class CartViewModel
    {
        public IEnumerable<Games> MyCartItems { get; set; }

        public  Dictionary<string, IEnumerable<Games>> FriendCartItems { get; set; }
    }
}
