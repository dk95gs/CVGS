using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CVGS.Models
{
    public class Reviews
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Body { get; set; }
        [Display(Name = "Creation Date")]
        public DateTime CreationDate { get; set; }
        [Required]
        public string ApplicationUserId { get; set; }

        public bool isApproved { get; set; }

        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        [Required]
        public int GameId { get; set; }

        [ForeignKey("GameId")]
        public virtual Games Games { get; set; }
    }
}
