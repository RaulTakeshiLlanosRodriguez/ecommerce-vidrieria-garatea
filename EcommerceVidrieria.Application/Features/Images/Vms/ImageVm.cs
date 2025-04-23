using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceVidrieria.Application.Features.Images.Vms
{
    public class ImageVm
    {
        public int Id { get; set; }
        public string? Url { get; set; }
        public int ProductId { get; set; }
        public string? PublicCode { get; set; }
    }
}
