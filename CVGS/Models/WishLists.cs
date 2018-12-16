using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CVGS.Models
{
    public class WishLists
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int GameId { get; set; }

        [ForeignKey("GameId")]
        public virtual Games Games { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }

        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }


    }
}
