using System;
using System.Collections.Generic;

namespace EFCoreDemo.Entities
{
    public class ProductCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }

        public List<Product> Products { get; set; } = new List<Product>();
    }
}
