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

        public virtual List<User> Users { get; set; } = new List<User>();
        public virtual List<Product> Products { get; set; } = new List<Product>();
        public virtual List<SupplyHistory> SupplyHistory { get; set; } = new List<SupplyHistory>();
    }
}
