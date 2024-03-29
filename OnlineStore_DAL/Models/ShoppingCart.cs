﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore_DAL.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        [Required] [ForeignKey("UserId")] public User User { get; set; }

        public List<Product> Products { get; set; }
    }
}