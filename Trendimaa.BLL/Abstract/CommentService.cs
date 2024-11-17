using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Trendeimaa.Entities;
using Trendimaa.BLL.Interface;
using Trendimaa.Common;
using Trendimaa.DAL.Context;
using Trendimaa.DAL.UnitOfWork;
using Trendimaa.DTO;
using Trendimaa.DTO.Listing;
using Trendimaa.DTO.Product;

namespace Trendimaa.BLL.Abstract
{
    public class CommentService : Service<Comment>, ICommentService
    {
        public readonly IMapper _mapper;
        public readonly TrendimaaContext _context;
        public readonly IValidator<Comment> _validator;
        public readonly IUOW _uow;
        public CommentService(IValidator<Comment> validator, IUOW uow,IMapper mapper ,TrendimaaContext context) : base(validator, uow, context)
        {
            _context = context;
            _mapper = mapper;
            _validator = validator;
            _uow = uow;
        }

        public async Task<IResponse<List<BasicProductCardDTO>>> GetNonCommentedProducts(int userId)
        {
            var products = await _context.Orders
                .Where(i => i.AppUserId == userId)                    // Kullanıcının siparişlerini filtrele
                .SelectMany(i => i.OrderItems)                        // Her siparişteki ürünleri al
                .Select(i => i.Product)                               // Ürünleri seç
                .Where(i => !i.Comments.Any(c => c.AppUserId == userId)) // Kullanıcı tarafından yorum yapılmamış ürünleri seç
                .ToListAsync();
           var mapped=_mapper.Map<List<BasicProductCardDTO>>(products);
            return new Response<List<BasicProductCardDTO>>(ResponseType.Success,mapped);
        }

        public async Task<IResponse<ListingDTO<CommentDTO>>> GetProductCommentsWithCount(int productId, int page, int quantity)
        {
            var data = await _context.Comments.Where(i => i.Product.SellerId == productId).Include(i => i.Images).AsNoTracking().ToListAsync();
            var mapped = _mapper.Map<List<CommentDTO>>(data);
            ListingDTO<CommentDTO> dto = new ListingDTO<CommentDTO>()
            {
                Count = data.Count,
                List = mapped.Skip((page - 1) * quantity).Take(quantity).ToList(),
            };

            return new Response<ListingDTO<CommentDTO>>(ResponseType.Success, dto);

        }

        public async Task<IResponse<ListingDTO<CommentDTO>>> GetSellerCommentsWithCount(int sellerId, int page, int quantity)
        {
            var data = await _context.Comments.Where(i => i.Product.SellerId == sellerId).Include(i => i.Images).AsNoTracking().ToListAsync();
           var mapped= _mapper.Map<List<CommentDTO>>(data);
            ListingDTO<CommentDTO> dto = new ListingDTO<CommentDTO>()
            {
                Count=data.Count,
                List= mapped.Skip((page - 1) * quantity).Take(quantity).ToList(),
            };

            return new Response<ListingDTO<CommentDTO>>(ResponseType.Success, dto);
        }
    }
}
