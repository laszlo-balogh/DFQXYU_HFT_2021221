using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFQXYU_HFT_2021221.Models
{
    [Table("Costumers")]
    public class Costumer
    {
        [Key]
        public int CostumerID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime BornDate { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [MaxLength(9)]
        [MinLength(9)]
        public int PhoneNumber { get; set; }
        public Costumer()
        {

        }
    }
}
