using FluentValidation;
using Trendeimaa.Entities;
using Trendimaa.BLL.Interface;
using Trendimaa.DAL.Context;
using Trendimaa.DAL.UnitOfWork;

namespace Trendimaa.BLL.Abstract
{
    public class AddressService : Service<Address>, IAddressService
    {
        public AddressService(IValidator<Address> validator, IUOW uow, TrendimaaContext context) : base(validator, uow, context)
        {
        }
    }
}
