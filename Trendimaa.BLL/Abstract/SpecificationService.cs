using FluentValidation;
using Trendeimaa.Entities;
using Trendimaa.BLL.Interface;
using Trendimaa.DAL.Context;
using Trendimaa.DAL.UnitOfWork;

namespace Trendimaa.BLL.Abstract
{
    public class SpecificationService : Service<Specification>, ISpecificationService
    {
        public SpecificationService(IValidator<Specification> validator, IUOW uow, TrendimaaContext context) : base(validator, uow, context)
        {
        }
    }
}
