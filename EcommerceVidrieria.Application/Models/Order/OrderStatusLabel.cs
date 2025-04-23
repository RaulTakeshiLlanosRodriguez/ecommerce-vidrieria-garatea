using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceVidrieria.Application.Models.Order
{
    public class OrderStatusLabel
    {
        public const string PENDIENTE = nameof(PENDIENTE);
        public const string ANULADO = nameof(ANULADO);
        public const string COMPLETADO = nameof(COMPLETADO);
    }
}
