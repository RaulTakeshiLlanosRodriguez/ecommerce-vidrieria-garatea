using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceVidrieria.Application.Features.Reports.Vms
{
    public class GetTotalSalesByMonthVm
    {
        public decimal TotalSales { get; set; }
        public int TotalOrdersCurrentMonth { get; set; }
        public decimal MaxOrderSale { get; set; }
    }
}
