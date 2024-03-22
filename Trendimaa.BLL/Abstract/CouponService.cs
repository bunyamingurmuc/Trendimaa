using FluentValidation;
using Trendeimaa.Entities;
using Trendimaa.BLL.Interface;
using Trendimaa.DAL.Context;
using Trendimaa.DAL.UnitOfWork;

namespace Trendimaa.BLL.Abstract
{
    public class CouponService : Service<Coupon>, ICouponService
    {
        public CouponService(IValidator<Coupon> validator, IUOW uow, TrendimaaContext context) : base(validator, uow, context)
        {

        }
        

    }
}
