using System;
using System.Collections.Generic;

namespace OnlineStore_DAL.Models
{
    public class Receipt
    {
        public int Id { get; set; }

        public int User { get; set; }

        public List<Product> Products { get; set; }

        public DateTime Date { get; set; }

        public string City { get; set; }

        public string Address { get; set; }
    }
}