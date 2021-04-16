using System;
using System.Collections.Generic;

namespace EFCoreDemo.Entities
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? Revenue { get; set; }
        public DateTime FoundationDate { get; set; }

        public List<User> Users { get; set; } = new List<User>();
        public List<Product> Products { get; set; } = new List<Product>();
        public List<SupplyHistory> SupplyHistory { get; set; } = new List<SupplyHistory>();
    }
}
