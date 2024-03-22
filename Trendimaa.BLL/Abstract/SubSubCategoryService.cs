using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trendeimaa.Entities.CategoryFolder;
using Trendimaa.BLL.Interface;
using Trendimaa.DAL.Context;
using Trendimaa.DAL.UnitOfWork;

namespace Trendimaa.BLL.Abstract
{
    public class SubSubCategoryService : Service<SubSubCategory>, ISubSubCategoryService
    {
        public SubSubCategoryService(IValidator<SubSubCategory> validator, IUOW uow, TrendimaaContext context) : base(validator, uow, context)
        {
        }
    }
}
