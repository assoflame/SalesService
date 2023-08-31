﻿namespace SalesService.Entities.Models
{
    public class Product
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsSold { get; set; }
        public DateTime CreationDate { get; set; }


        public User User { get; set; }
        public IEnumerable<ProductImage> Images { get; set; }
    }
}
