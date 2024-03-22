using FluentValidation;
using Trendeimaa.Entities;
using Trendimaa.BLL.Interface;
using Trendimaa.DAL.Context;
using Trendimaa.DAL.UnitOfWork;

namespace Trendimaa.BLL.Abstract
{
    public class FavoriteService : Service<Favorite>, IFavoriteService
    {
        public FavoriteService(IValidator<Favorite> validator, IUOW uow, TrendimaaContext context) : base(validator, uow, context)
        {
        }
    }
}
