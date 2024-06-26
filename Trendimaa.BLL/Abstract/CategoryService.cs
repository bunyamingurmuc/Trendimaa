using AutoMapper;
using Azure;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Trendeimaa.Entities;
using Trendimaa.BLL.Interface;
using Trendimaa.Common;
using Trendimaa.Common.Enum;
using Trendimaa.DAL.Context;
using Trendimaa.DAL.UnitOfWork;
using Trendimaa.DTO.CategoryDTOs;

namespace Trendimaa.BLL.Abstract
{
    public class CategoryService : Service<Category>, ICategoryService
    {
        public readonly IMapper _mapper;
        public readonly TrendimaaContext _context;
        public readonly IValidator<Category> _validator;
        public readonly IUOW _uow;
        public CategoryService(IValidator<Category> validator, IUOW uow, TrendimaaContext context, IMapper mapper) : base(validator, uow, context)
        {
            _validator = validator;
            _uow = uow;
            _context = context;
            _mapper = mapper;
        }

        public async Task<IResponse<List<CategoryHomeDTO>>> GetCategories(Language language)
        {
            var cats = await _context.Categories
                .Include(i=>i.Images)
                .ToListAsync();
            var mapped = _mapper.Map<List<CategoryHomeDTO>>(cats);
            return new Common.Response<List<CategoryHomeDTO>>(ResponseType.Success, mapped);
        }

        public async Task<IResponse<List<CategoryHomeDTO>>> GetMainHomeCategories(Language language)
        {
            var cats =await _context.Categories
                .Include(i=>i.Images)
                .ToListAsync();
            var mapped = _mapper.Map<List<CategoryHomeDTO>>(cats);
            return new Common.Response<List<CategoryHomeDTO>>(ResponseType.Success, mapped);
        }
    }
}
