using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore_DAL.Models
{
    public class Receipt
    {
        public int Id { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }

        public User User { get; set; }

        public List<Product> Products { get; set; }

        public DateTime Date { get; set; }

        public string City { get; set; }

        public string Address { get; set; }
    }
}