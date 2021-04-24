using System.Collections.Generic;

namespace EFCoreDemo.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int CategoryId { get; set; }
        public virtual ProductCategory Category { get; set; }

        public virtual List<Company> Companies { get; set; } = new List<Company>();
        public virtual List<SupplyHistory> SupplyHistory { get; set; } = new List<SupplyHistory>();
    }
}
