using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceVidrieria.Application.Features.Reports.Vms
{
    public class TotalOrdersVm
    {
        public int TotalOrders { get; set; }
        public int TotalPendingOrders { get; set; }
        public int TotalCompletedOrders { get; set; }
        public int TotalCancelledOrders { get; set; }
        public List<CityOrdersVm>? OrdersByCity { get; set; }

    }
}
