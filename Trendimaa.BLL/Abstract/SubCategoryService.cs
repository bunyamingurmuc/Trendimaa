using FluentValidation;
using Trendeimaa.Entities.CategoryFolder;
using Trendimaa.BLL.Interface;
using Trendimaa.DAL.Context;
using Trendimaa.DAL.UnitOfWork;

namespace Trendimaa.BLL.Abstract
{
    internal class SubCategoryService : Service<SubCategory>, ISubCategoryService
    {
        public SubCategoryService(IValidator<SubCategory> validator, IUOW uow, TrendimaaContext context) : base(validator, uow, context)
        {
        }
    }
}
