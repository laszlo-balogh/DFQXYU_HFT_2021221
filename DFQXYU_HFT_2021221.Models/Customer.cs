﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
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
        [MaxLength(9)]
        [MinLength(9)]
        public int PhoneNumber { get; set; }
        [NotMapped]
        public virtual ICollection<MovieRental> Rentals { get; set; }
        public bool RegularCustomer { get; set; }
        public Customer()
        {
            this.Rentals = new HashSet<MovieRental>();
            this.RegularCustomer = false;
        }

    }
}