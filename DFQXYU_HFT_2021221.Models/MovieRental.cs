using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DFQXYU_HFT_2021221.Models
{
    [Table("Rentals")]
    public class MovieRental
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RentalID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Promotions { get; set; }
        [JsonIgnore]
        [NotMapped]
        public virtual Movie Movie { get; set; }
        [ForeignKey(nameof(Models.Movie))]

        public int MovieID { get; set; }
        [JsonIgnore]
        [NotMapped]
        public virtual Customer Customer { get; set; }
        [ForeignKey(nameof(Models.Customer))]
        public int CustomerID { get; set; }
        public MovieRental()
        {
            this.StartDate = DateTime.Now;
            this.EndDate = this.StartDate.AddDays(30);
            //    this.CustomerID = Customer.CustomerID;
            //    this.MovieID = Movie.MovieID;
        }

        public override string ToString()
        {
            string s = $"RentalID = {RentalID} - StartDate = {StartDate} - EndDate = {EndDate}" +
                $"- Prmotions = {Promotions} - MovieID = {MovieID} - CustomerID = {CustomerID}";
            return s;
        }
    }
}
