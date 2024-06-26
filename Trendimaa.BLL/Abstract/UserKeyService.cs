using FluentValidation;
using Trendeimaa.Entities;
using Trendimaa.BLL.Interface;
using Trendimaa.DAL.Context;
using Trendimaa.DAL.UnitOfWork;

namespace Trendimaa.BLL.Abstract
{
    public class UserKeyService : Service<UserKey>, IUserKeyService
    {
        public UserKeyService(IValidator<UserKey> validator, IUOW uow, TrendimaaContext context) : base(validator, uow, context)
        {
        }
    }
}
