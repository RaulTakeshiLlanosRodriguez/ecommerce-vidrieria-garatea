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

namespace EcommerceVidrieria.Application.Features.Products.Command.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductVm>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ProductVm> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var productToUpdate = await _unitOfWork.Repository<Product>().GetByIdAsync(request.Id);

            if (productToUpdate == null)
            {
                throw new NotFoundException(nameof(Product), request.Id);
            }

            _mapper.Map(request, productToUpdate, typeof(UpdateProductCommand), typeof(Product));
            await _unitOfWork.Repository<Product>().UpdateAsync(productToUpdate);

            if ((request.ImageUrls != null) && (request.ImageUrls.Count > 0))
            {
                var imagesToRemove = await _unitOfWork.Repository<Image>().GetAsync(
                    x => x.ProductId == request.Id);

                _unitOfWork.Repository<Image>().DeleteRange(imagesToRemove);
                request.ImageUrls.Select(x =>
                {
                    x.ProductId = request.Id;
                    return x;
                }).ToList();
                var images = _mapper.Map<List<Image>>(request.ImageUrls);
                _unitOfWork.Repository<Image>().AddRange(images);
                await _unitOfWork.Complete();
            }

            return _mapper.Map<ProductVm>(productToUpdate);
        }
    }
}
