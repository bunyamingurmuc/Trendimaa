using AutoMapper;
using FluentValidation;
using Trendeimaa.Entities;
using Trendimaa.BLL.Extension;
using Trendimaa.BLL.Interface;
using Trendimaa.Common;
using Trendimaa.DAL.Context;
using Trendimaa.DAL.UnitOfWork;

namespace Trendimaa.BLL.Abstract
{
    public class SellerService : Service<Seller>, ISellerService
    {
        public readonly IValidator<Seller> _validator;
        public readonly IUOW _uow;
        public readonly TrendimaaContext _context;
        public readonly IMapper _mapper;
        public SellerService(IValidator<Seller> validator, IUOW uow, TrendimaaContext context, IMapper mapper) : base(validator, uow, context)
        {
            _validator = validator;
            _uow = uow;
            _context = context;
            _mapper = mapper;
        }

        public async Task<IResponse<Seller>> SignUp(Seller seller)
        {
            var result = _validator.Validate(seller);
            if (!result.IsValid)
                return new Response<Seller>(seller, errors: result.ConvertToCustomValidationError());

            var rnd = new Random();
            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            seller.Password = BCrypt.Net.BCrypt.HashPassword(seller.Password, salt);

            var data = await _uow.GetRepository<Seller>().CreateAsync(seller);
            return new Response<Seller>(ResponseType.Success, data);
        }
    }
}
