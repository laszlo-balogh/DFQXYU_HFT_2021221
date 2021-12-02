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
    [Table("Customers")]
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime BornDate { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]      
        public int PhoneNumber { get; set; }
        [JsonIgnore]
        [NotMapped]
        public virtual ICollection<MovieRental> Rentals { get; set; }
        public bool RegularCustomer { get; set; }
        public Customer()
        {
            this.Rentals = new HashSet<MovieRental>();
            this.RegularCustomer = false;
        }

        public override string ToString()
        {
            string s = $"CustomerID = {CustomerID} - Name = {Name} - BornDate = {BornDate}" +
                $"- Email = {Email} - PhoneNumber = {PhoneNumber} - RegularCustomer = {RegularCustomer}";
            return s;
        }
    }
}
