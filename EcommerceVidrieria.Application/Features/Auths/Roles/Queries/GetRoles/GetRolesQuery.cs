using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceVidrieria.Application.Features.Auths.Roles.Queries.GetRoles
{
    public class GetRolesQuery:IRequest<List<string>>
    {
    }
}
