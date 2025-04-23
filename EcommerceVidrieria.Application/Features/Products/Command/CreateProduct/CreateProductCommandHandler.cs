﻿using AutoMapper;
using EcommerceVidrieria.Application.Features.Products.Vms;
using EcommerceVidrieria.Application.Persistence;
using EcommerceVidrieria.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceVidrieria.Application.Features.Products.Command.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductVm>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ProductVm> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var productEntity = _mapper.Map<Product>(request);
            await _unitOfWork.Repository<Product>().AddAsync(productEntity);

            if (request.ImageUrls != null && request.ImageUrls.Count() > 0)
            {
                request.ImageUrls.Select(c =>
                {
                    c.ProductId = productEntity.Id;
                    return c;
                }).ToList();
                var images = _mapper.Map<List<Image>>(request.ImageUrls);
                _unitOfWork.Repository<Image>().AddRange(images);
                await _unitOfWork.Complete();
            }

            return _mapper.Map<ProductVm>(productEntity);
        }
    }
}
