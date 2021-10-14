using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFQXYU_HFT_2021221.Models
{
    public class Movie
    {
        [Key]
        public string MovieID { get; set; }
        [Required]
        public string MovieTitle { get; set; }
        public int Year { get; set; }
        public string Producer { get; set; }
        public string Location { get; set; }
        [Required]
        public int Price { get; set; }



    }
}
