﻿using EcommerceVidrieria.Application.Features.Auths.Users.Vms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceVidrieria.Application.Features.Auths.Users.Queries.GetUserByToken
{
    public class GetUserByTokenQuery : IRequest<AuthResponse>
    {
    }
}
