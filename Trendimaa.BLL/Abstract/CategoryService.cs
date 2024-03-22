using FluentValidation;
using Trendeimaa.Entities;
using Trendimaa.BLL.Interface;
using Trendimaa.DAL.Context;
using Trendimaa.DAL.UnitOfWork;

namespace Trendimaa.BLL.Abstract
{
    public class CategoryService : Service<Category>, ICategoryService
    {
        public CategoryService(IValidator<Category> validator, IUOW uow, TrendimaaContext context) : base(validator, uow, context)
        {

        }
    }
}
