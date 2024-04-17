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
    public class SpecificationService : Service<Specification>, ISpecificationService
    {
        public readonly IMapper _mapper;
        public readonly TrendimaaContext _context;
        public readonly IValidator<Specification> _validator;
        public readonly IUOW _uow;
        public SpecificationService(IValidator<Specification> validator, IUOW uow, IMapper mapper, TrendimaaContext context) : base(validator, uow, context)
        {
            _context = context;
            _mapper = mapper;
            _validator = validator;
            _uow = uow;
        }

        public async Task<IResponse<List<Specification>>> GetProductSpecifications(int productId)
        {
            var list = await _context.Specifications.Where(i => i.ProductId == productId).AsNoTracking().ToListAsync();
            return new Response<List<Specification>>(ResponseType.Success, list);

        }

        public async Task<IResponse<List<Specification>>> GetSpecificationsForSearch(int? subcatid, int? subsubcatid)
        {
            var list = await _context.Specifications.ToListAsync();
            
            if (subcatid != null)
                list = list.Where(i => i.SubCategoryId == subcatid).ToList();
            if (subsubcatid != null)
                list = list.Where(i => i.SubSubCategoryId == subsubcatid).ToList();
             list = list.OrderBy(i => i.Title).ToList();
            return new Response<List<Specification>>(ResponseType.Success, list);

        }
    }
}
