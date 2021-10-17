using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFQXYU_HFT_2021221.Models
{
    public class Movie
    {
        [Key]
        public int MovieID { get; set; }
        [Required]
        public string MovieTitle { get; set; }
        [MaxLength(4)]
        public int Year { get; set; }
        public string Producer { get; set; }
        public string Location { get; set; }
        [Required]
        public int? Price { get; set; }
        [NotMapped]
        public virtual ICollection<MovieRental> Rentals { get; set; }

        public Movie()
        {
            this.Rentals = new HashSet<MovieRental>();
        }
    }
}
