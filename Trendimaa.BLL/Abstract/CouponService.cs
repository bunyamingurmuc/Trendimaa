using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.IO.Compression;
using Trendeimaa.Entities;
using Trendimaa.BLL.Interface;
using Trendimaa.Common;
using Trendimaa.DAL.Context;
using Trendimaa.DAL.UnitOfWork;
using Trendimaa.DTO;
using Trendimaa.DTO.Product;

namespace Trendimaa.BLL.Abstract
{
    public class CouponService : Service<Coupon>, ICouponService
    {
        public readonly TrendimaaContext _context;
        public readonly IValidator<Coupon> _validator;
        private readonly IUOW _uow;
        private readonly IMapper _mapper;
        public CouponService(IValidator<Coupon> validator, IUOW uow, IMapper mapper, TrendimaaContext context) : base(validator, uow, context)
        {
            _context = context;
            _uow = uow;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<Common.Response<List<ImageDTO>>> GetCouponProductImages(int couponId)
        {
            Random rnd = new Random();
            var productImages = new List<Image>();
            var coupon = await _context.Coupons.Where(i => i.Id == couponId)
                .Include(i => i.CouponOffer).ThenInclude(i => i.SellerCouponOffers).ThenInclude(i => i.Seller).ThenInclude(i => i.Products)
                .FirstOrDefaultAsync();
            var couponOffer = await _context.CouponOffers.Where(i => i.Coupons.Contains(coupon)).FirstOrDefaultAsync();
            if (couponOffer.IsJustForSeller == false)
                productImages = await _context.Products.Take(100).OrderBy(i => rnd.Next()).Select(i => i.Images[0]).Take(5).ToListAsync();
            else
                productImages = couponOffer.SellerCouponOffers.SelectMany(i => i.Seller.Products.Select(i => i.Images[0])).Take(5).ToList();
            var mapped = _mapper.Map<List<ImageDTO>>(productImages);

            return new Response<List<ImageDTO>>(ResponseType.Success, mapped);

        }

        public async Task<Common.Response<List<CouponDTO>>> GetUserCoupons(int userId)
        {
            var coupons = await _context.Coupons.Where(i => i.AppUserId == userId).ToListAsync();
            var mapped = _mapper.Map<List<CouponDTO>>(coupons);
            return new Common.Response<List<CouponDTO>>(ResponseType.Success, mapped);
        }

        public async Task<Common.Response<List<BasicProductCardDTO>>> GetCouponProducts(int couponId)
        {
            var coupon = await _context.Coupons.Where(i => i.Id == couponId).FirstOrDefaultAsync();

            var couponOffer = await _context.Coupons.Where(i => i.Id != couponId).Select(i => i.CouponOffer).FirstOrDefaultAsync();
            if (couponOffer.IsJustForSeller == true)
            {
                var products = await _context.Coupons.Where(i => i.Id != couponId).SelectMany(i => i.CouponOffer.SellerCouponOffers.Select(i=>i.Seller.Products)).FirstOrDefaultAsync();
                var mapped = _mapper.Map<List<BasicProductCardDTO>>(products);
                return new Response<List<BasicProductCardDTO>>(ResponseType.Success, mapped);
            }

            return new Response<List<BasicProductCardDTO>>(ResponseType.ValidationError, "");
        }
    }
}
