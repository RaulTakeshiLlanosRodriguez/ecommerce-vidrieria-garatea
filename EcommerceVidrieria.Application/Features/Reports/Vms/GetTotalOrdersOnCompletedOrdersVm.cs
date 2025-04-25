using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceVidrieria.Application.Features.Reports.Vms
{
    public class GetTotalOrdersOnCompletedOrdersVm
    {
        public int TotalOrdersCurrentMonth { get; set; }
        public int CompletedOrders { get; set; }
        public int PendingOrders { get; set; }
        public decimal CompletionPercentage { get; set; }
    }
}
