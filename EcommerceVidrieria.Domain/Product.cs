using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceVidrieria.Domain
{
    public class Product : BaseDomainModel
    {
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public int Valorization { get; set; } = 0;
        public int Stock { get; set; }
        public ProductStatus Status { get; set; } = ProductStatus.Active;
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public virtual ICollection<Image>? Images { get; set; }
    }
}
