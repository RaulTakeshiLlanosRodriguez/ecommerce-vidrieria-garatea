using AutoMapper;
using EcommerceVidrieria.Application.Exceptions;
using EcommerceVidrieria.Application.Features.Products.Vms;
using EcommerceVidrieria.Application.Persistence;
using EcommerceVidrieria.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceVidrieria.Application.Features.Products.Command.UpdateAdminStatusProduct
{
    public class UpdateAdminStatusProductCommandHandler : IRequestHandler<UpdateAdminStatusProductCommand, ProductVm>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateAdminStatusProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ProductVm> Handle(UpdateAdminStatusProductCommand request, CancellationToken cancellationToken)
        {
            var productToDelete = await _unitOfWork.Repository<Product>().GetByIdAsync(request.ProductId);

            if (productToDelete == null)
            {
                throw new NotFoundException(nameof(Product), request.ProductId);
            }

            productToDelete.Status = productToDelete.Status == ProductStatus.Inactive ? ProductStatus.Active : ProductStatus.Inactive;
            await _unitOfWork.Repository<Product>().UpdateAsync(productToDelete);

            return _mapper.Map<ProductVm>(productToDelete);
        }
    }
}
