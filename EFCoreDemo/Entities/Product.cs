using System.Collections.Generic;

namespace EFCoreDemo.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Company> Companies { get; set; } = new List<Company>();
        public List<SupplyHistory> SupplyHistory { get; set; } = new List<SupplyHistory>();
    }
}
