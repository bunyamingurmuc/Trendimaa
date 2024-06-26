using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Trendeimaa.Entities;
using Trendimaa.BLL.Interface;
using Trendimaa.Common;
using Trendimaa.DAL.Context;
using Trendimaa.DAL.UnitOfWork;

namespace Trendimaa.BLL.Abstract
{
    public class AddressService : Service<Address>, IAddressService
    {
        public readonly IMapper _mapper;
        public readonly TrendimaaContext _context;
        public readonly IValidator<Address> _validator;
        public readonly IUOW _uow;
        public AddressService(IValidator<Address> validator, IUOW uow, IMapper mapper, TrendimaaContext context) : base(validator, uow, context)
        {
            _mapper = mapper;
            _validator = validator;
            _uow = uow;
            _context = context;

        }

        public async Task<IResponse<List<Address>>> GetUserAddresses(int id)
        {
            var addresses=await _context.Addresses.Where(i=>i.AppUserId == id).ToListAsync();
            return new Response<List<Address>>(ResponseType.Success,addresses);
        }
    }
}
