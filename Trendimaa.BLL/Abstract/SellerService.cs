using AutoMapper;
using FluentValidation;
using Orde.BLL.Extension.Token;
using Trendeimaa.Entities;
using Trendimaa.BLL.Extension;
using Trendimaa.BLL.Extension.Token;
using Trendimaa.BLL.Interface;
using Trendimaa.Common;
using Trendimaa.DAL.Context;
using Trendimaa.DAL.UnitOfWork;
using Trendimaa.DTO;

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
        public async Task<IResponse<JwtTokenResponse>> LogIn(CSellerLoginDto dto)
        {
            var sellers = await _uow.GetRepository<Seller>().GetAllAsync();
            var seller = sellers.FirstOrDefault(i => i.EMail == dto.Email);
            if (seller == null)
                return new Response<JwtTokenResponse>(ResponseType.NotFound, "Kullanıcı bulunamadı");
            bool isValidPassword = BCrypt.Net.BCrypt.Verify(dto.Password, seller.Password);
            if (isValidPassword == false)
                return new Response<JwtTokenResponse>(ResponseType.NotFound, "Kullanıcı adı veya porala yanış");
            if (seller.IsActive == false)
                return new Response<JwtTokenResponse>(ResponseType.NotFound, "Üyeliğiniz daha aktif edilmedi");

            dto.Id = seller.Id;
            dto.Role = "Seller";
            dto.Currency = seller.Currency;
            dto.Language = seller.Language;
            var token = JwtTokenGenerator.GenerateTokenSeller(dto);
            return new Response<JwtTokenResponse>(ResponseType.Success, token);
        }
        public async Task<IResponse> up()
        {
            var seller = _context.Sellers.Where(i => i.Id == 2).FirstOrDefault();
            var unc = seller;
            seller.IsActive = true;
            _uow.GetRepository<Seller>().Update(seller, unc);
            _uow.SaveChangesAsync();
            return new Response(ResponseType.Success);
        }
    }
}
