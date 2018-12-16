using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CVGS.Models
{
    public class Games
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Genre { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Developer { get; set; }
        [Required]
        public string Publisher { get; set; }
        [Required]
        [Display(Name = "ESRB Ratings")]
        public string ESRB_Ratings { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        [Required]
        public double Price { get; set; }
        public string GameLink { get; set; }
        public string Available { get; set; }
        public float Rating { get; set; }
        public bool isFree { get; set; }
        public bool isDownloadable { get; set; }
        public string ExeLink { get; set; }     

    }
}
