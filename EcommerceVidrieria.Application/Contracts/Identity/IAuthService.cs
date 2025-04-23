using EcommerceVidrieria.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceVidrieria.Application.Contracts.Identity
{
    public interface IAuthService
    {
        string GetSessionUser();
        string CreateToken(User user, IList<string>? roles);
    }
}
