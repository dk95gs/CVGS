using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CVGS.Models
{
    public class FriendsFamilyLists
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string ParentUserName { get; set; }

        [Required]
        public string ChildUserId { get; set; }

        [ForeignKey("ChildUserId")]
        public virtual ApplicationUser ChildApplicationUser { get; set; }

    }
}
