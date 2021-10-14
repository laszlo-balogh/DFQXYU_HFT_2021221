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
    class Costumer
    {
        [Key]
        public int CostumerID { get; set; }
        public string Name { get; set; }
        public DateTime BornDate { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }       
    }
}
