using EcommerceVidrieria.Application.Features.Categories.Vms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceVidrieria.Application.Features.Categories.Queries.GetCategoryList
{
    public class GetCategoryListQuery : IRequest<IReadOnlyList<CategoryVm>>
    {
    }
}
