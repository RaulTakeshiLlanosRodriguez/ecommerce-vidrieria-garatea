using AutoMapper;
using EcommerceVidrieria.Application.Features.Cities.Vms;
using EcommerceVidrieria.Application.Persistence;
using EcommerceVidrieria.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceVidrieria.Application.Features.Cities.Queries.GetCities
{
    public class GetCitiesQueryHandler : IRequestHandler<GetCitiesQuery, IReadOnlyList<CityVm>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCitiesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<CityVm>> Handle(GetCitiesQuery request, CancellationToken cancellationToken)
        {
            var cities = await _unitOfWork.Repository<City>().GetAsync(null, x => x.OrderBy(x => x.NameCity), string.Empty, false);
            return _mapper.Map<IReadOnlyList<CityVm>>(cities);
        }
    }
}
