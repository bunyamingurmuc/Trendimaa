using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Trendeimaa.Entities;
using Trendimaa.BLL.Interface;
using Trendimaa.Common;
using Trendimaa.DAL.Context;
using Trendimaa.DAL.UnitOfWork;
using Trendimaa.DTO;

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
        public async Task<IResponse<List<CommentDTO>>> GetProductComments(int productId)
        {
            var comments = await _context.Comments.Where(i=>i.ProductId==productId).Include(i => i.Images).AsNoTracking().ToListAsync();
            var mapped = _mapper.Map<List<CommentDTO>>(comments);
            return new Response<List<CommentDTO>>(ResponseType.Success, mapped);

        }

        public async Task<IResponse<List<CommentDTO>>> GetSellerComments(int sellerId)
        {
            var comments = await _context.Comments.Where(i => i.Product.SellerId == sellerId).Include(i => i.Images).AsNoTracking().ToListAsync();
            var mapped = _mapper.Map<List<CommentDTO>>(comments);
            return new Response<List<CommentDTO>>(ResponseType.Success, mapped);
        }
    }
}
