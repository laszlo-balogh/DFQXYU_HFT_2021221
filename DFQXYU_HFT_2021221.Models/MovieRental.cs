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
        }

        public override string ToString()
        {
            string s = $"RentalID = {RentalID} - StartDate = {StartDate} - EndDate = {EndDate}" +
                $"- Prmotions = {Promotions} - MovieID = {MovieID} - CustomerID = {CustomerID}";
            return s;
        }

        public override bool Equals(object obj)
        {
            MovieRental mr = obj as MovieRental;
            return mr.RentalID == this.RentalID && mr.MovieID == this.MovieID && mr.CustomerID == this.CustomerID
                && mr.StartDate == this.StartDate && mr.EndDate == this.EndDate && mr.Promotions == this.Promotions;
        }

        public override int GetHashCode()
        {
            return this.RentalID + this.MovieID + this.CustomerID + this.StartDate.Year + this.StartDate.Day + this.EndDate.Year
                 + this.EndDate.Day + this.StartDate.Month + this.EndDate.Month;
        }
    }
}
