using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceVidrieria.Domain
{
    public class Image : BaseDomainModel
    {
        public string? Url { get; set; }
        public int ProductId { get; set; }
        public string? PublicCode { get; set; }
        public Product? Product { get; set; }
    }
}
