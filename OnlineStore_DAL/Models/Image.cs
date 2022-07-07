﻿namespace OnlineStore_DAL.Models
{
    public class Image
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}