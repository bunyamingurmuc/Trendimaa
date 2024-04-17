using FluentValidation;
using Trendeimaa.Entities;
using Trendimaa.BLL.Interface;
using Trendimaa.DAL.Context;
using Trendimaa.DAL.UnitOfWork;

namespace Trendimaa.BLL.Abstract
{
    public class WalletItemService : Service<WalletItem>, IWalletItemService
    {
        public WalletItemService(IValidator<WalletItem> validator, IUOW uow, TrendimaaContext context) : base(validator, uow, context)
        {
        }
    }
}
