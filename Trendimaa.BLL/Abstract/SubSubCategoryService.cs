using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Trendeimaa.Entities.CategoryFolder;
using Trendimaa.BLL.Interface;
using Trendimaa.Common;
using Trendimaa.Common.Enum;
using Trendimaa.DAL.Context;
using Trendimaa.DAL.UnitOfWork;
using Trendimaa.DTO.CategoryDTOs;

namespace Trendimaa.BLL.Abstract
{
    public class SubSubCategoryService : Service<SubSubCategory>, ISubSubCategoryService
    {
        public readonly IMapper _mapper;
        public readonly TrendimaaContext _context;
        public readonly IValidator<SubSubCategory> _validator;
        public readonly IUOW _uow;
        public SubSubCategoryService(IValidator<SubSubCategory> validator, IUOW uow, TrendimaaContext context, IMapper mapper) : base(validator, uow, context)
        {
            _context = context;
            _mapper = mapper;
            _validator = validator;
            _uow = uow;
        }

        public async Task<IResponse<List<CategoryHomeDTO>>> GetSubSubCategories(Language language, int subCatId)
        {
            var cats = await _context.SubSubCategories
                .Where(i=>i.SubCategoryId==subCatId)
                .ToListAsync();
            var mapped = _mapper.Map<List<CategoryHomeDTO>>(cats);
            return new Common.Response<List<CategoryHomeDTO>>(ResponseType.Success, mapped);
        }

    }
}
