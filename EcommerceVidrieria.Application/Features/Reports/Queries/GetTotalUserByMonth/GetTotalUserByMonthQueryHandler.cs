using EcommerceVidrieria.Application.Features.Reports.Vms;
using EcommerceVidrieria.Application.Persistence;
using EcommerceVidrieria.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceVidrieria.Application.Features.Reports.Queries.GetTotalUserByMonth
{
    public class GetTotalUserByMonthQueryHandler : IRequestHandler<GetTotalUserByMonthQuery, ReportVm<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTotalUserByMonthQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ReportVm<int>> Handle(GetTotalUserByMonthQuery request, CancellationToken cancellationToken)
        {
            var totalUsers = await _unitOfWork.Repository<User>().GetAsync(u => u.CreatedDate.Month == request.Month && u.CreatedDate.Year == request.Year);
            var response = new ReportVm<int>
            {
                Result = totalUsers.Count()
            };

            return response;
        }
    }
}
