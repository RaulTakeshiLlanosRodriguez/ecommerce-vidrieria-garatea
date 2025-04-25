using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceVidrieria.Domain
{
    public class City : BaseDomainModel
    {
        public string? NameCity { get; set; }
        public IReadOnlyList<Order>? Orders { get; set; }
    }
}
