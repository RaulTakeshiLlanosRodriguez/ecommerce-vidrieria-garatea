using EcommerceVidrieria.Application.Features.Cities.Vms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceVidrieria.Application.Features.Cities.Queries.GetCities
{
    public class GetCitiesQuery : IRequest<IReadOnlyList<CityVm>>
    {
    }
}
