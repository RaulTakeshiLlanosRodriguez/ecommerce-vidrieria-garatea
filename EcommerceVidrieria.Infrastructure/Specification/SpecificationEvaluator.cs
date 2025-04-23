using EcommerceVidrieria.Application.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceVidrieria.Infrastructure.Specification
{
    public class SpecificationEvaluator<T> where T : class
    {
        public static IQueryable<T> GetQuery(IQueryable<T> query, ISpecification<T> specification)
        {
            if (specification.Criteria != null)
            {
                query = query.Where(specification.Criteria);
            }

            query = specification.Includes!
                .Aggregate(query, (current, include) => current.Include(include))
                .AsSplitQuery().AsNoTracking();
            return query;

        }
    }
}
