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
    [Table("Movies")]
    public class Movie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MovieID { get; set; }
        [Required]
        public string MovieTitle { get; set; }
        public int Year { get; set; }
        public string Producer { get; set; }
        public string Location { get; set; }
        [Required]
        public int? Price { get; set; }
        [JsonIgnore]
        [NotMapped]
        public virtual ICollection<MovieRental> Rentals { get; set; }

        public Movie()
        {
            this.Rentals = new HashSet<MovieRental>();
        }
        public override string ToString()
        {
            string s = $"MovieID = {MovieID} - MovieTitle = {MovieTitle} - Year = {Year}" +
                $"- Producer = {Producer} - Location = {Location} - Price = {Price}";
            return s;
        }        
    }
}
