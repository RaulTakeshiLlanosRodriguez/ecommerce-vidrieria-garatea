using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceVidrieria.Domain
{
    public class Category : BaseDomainModel
    {
        public string? NameCategory { get; set; }
        public string? TitleCategory { get; set; }
        public virtual ICollection<Product>? Products { get; set; }
    }
}
