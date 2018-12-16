using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVGS.Models.ViewModel
{
    public class ReviewsViewModel
    {
        public Reviews Reviews { get; set; }
        public IEnumerable<ApplicationUser> ApplicationUser { get; set; }

    }
}
