using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Trendeimaa.Entities;
using Trendeimaa.Entities.CategoryFolder;
using Trendimaa.BLL.Interface;
using Trendimaa.Common;
using Trendimaa.Common.Enum;
using Trendimaa.DAL.Context;
using Trendimaa.DAL.UnitOfWork;
using Trendimaa.DTO.CategoryDTOs;

namespace Trendimaa.BLL.Abstract
{
    internal class SubCategoryService : Service<SubCategory>, ISubCategoryService
    {
        public readonly IMapper _mapper;
        public readonly TrendimaaContext _context;
        public readonly IValidator<SubCategory> _validator;
        public readonly IUOW _uow;
        public SubCategoryService(IValidator<SubCategory> validator, IUOW uow, TrendimaaContext context, IMapper mapper) : base(validator, uow, context)
        {
            _validator = validator;
            _uow = uow;
            _context = context;
            _mapper = mapper;
        }

        public async Task<IResponse<List<CategoryHomeDTO>>> GetSubCategories(Language language)
        {
            var cats = await _context.SubCategoies.ToListAsync();
            var mapped = _mapper.Map<List<CategoryHomeDTO>>(cats);
            return new Common.Response<List<CategoryHomeDTO>>(ResponseType.Success, mapped);

        }
    }
}
