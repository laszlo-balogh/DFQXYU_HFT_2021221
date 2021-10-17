using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFQXYU_HFT_2021221.Models
{
    [Table("Rentals")]
    public class MovieRental
    {
        [Key]
        public int RentalID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Promotions { get; set; }
        [NotMapped]
        public Movie Movie { get; set; }
        [ForeignKey(nameof(Movie))]
        public int MovieID { get; set; }
        [NotMapped]
        public Costumer Costumer { get; set; }
        [ForeignKey(nameof(Costumer))]
        public int CostumerID { get; set; }

        

    }
}
