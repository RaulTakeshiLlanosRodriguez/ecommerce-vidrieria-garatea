using EcommerceVidrieria.Application.Features.Images.Vms;
using EcommerceVidrieria.Application.Models.Product;
using EcommerceVidrieria.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceVidrieria.Application.Features.Products.Vms
{
    public class ProductVm
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Valorization { get; set; }
        public int Stock { get; set; }
        public virtual ICollection<ImageVm>? Images { get; set; }
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public ProductStatus Status { get; set; }
        public string StatusLabel
        {
            get
            {
                switch (Status)
                {
                    case ProductStatus.Active:
                        {
                            return ProductStatusLabel.ACTIVO;
                        }
                    case ProductStatus.Inactive:
                        {
                            return ProductStatusLabel.INACTIVO;
                        }
                    default: return ProductStatusLabel.INACTIVO;
                }
            }
            set { }
        }
    }
}
