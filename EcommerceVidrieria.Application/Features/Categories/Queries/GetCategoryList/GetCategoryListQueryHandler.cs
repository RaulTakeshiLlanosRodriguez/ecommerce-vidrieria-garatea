using AutoMapper;
using EcommerceVidrieria.Application.Features.Categories.Vms;
using EcommerceVidrieria.Application.Persistence;
using EcommerceVidrieria.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceVidrieria.Application.Features.Categories.Queries.GetCategoryList
{
    public class GetCategoryListQueryHandler : IRequestHandler<GetCategoryListQuery, IReadOnlyList<CategoryVm>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCategoryListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<CategoryVm>> Handle(GetCategoryListQuery request, CancellationToken cancellationToken)
        {
            var categories = await _unitOfWork.Repository<Category>().GetAsync(null,
                x => x.OrderBy(x => x.NameCategory),
                string.Empty, false);

            return _mapper.Map<IReadOnlyList<CategoryVm>>(categories);
        }
    }
}
