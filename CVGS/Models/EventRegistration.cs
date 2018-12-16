using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CVGS.Models
{
    public class EventRegistration
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int EventId { get; set; }

        [ForeignKey("EventId")]
        public virtual Events Events { get; set; }
        [Required]
        public string ApplicationUserId { get; set; }

        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
