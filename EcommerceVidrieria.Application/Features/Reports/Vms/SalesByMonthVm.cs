using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceVidrieria.Application.Features.Reports.Vms
{
    public class SalesByMonthVm
    {
        public string? Month { get; set; }
        public int TotalProductsByMonth { get; set; }
        public decimal TotalSalesByMonth { get; set; }
    }
}
