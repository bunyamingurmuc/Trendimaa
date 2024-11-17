using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Trendeimaa.Entities;
using Trendimaa.BLL.Interface;
using Trendimaa.Common;
using Trendimaa.DAL.Context;
using Trendimaa.DAL.UnitOfWork;
using Trendimaa.DTO;
using Trendimaa.DTO.Product;

namespace Trendimaa.BLL.Abstract
{
    public class VarietyService : Service<Variety>, IVarietyService
    {
        public readonly IMapper _mapper;
        public readonly TrendimaaContext _context;
        public readonly IValidator<Variety> _validator;
        public readonly IUOW _uow;
        public VarietyService(IValidator<Variety> validator, IUOW uow,IMapper mapper, TrendimaaContext context) : base(validator, uow, context)
        {
            _validator = validator;
            _uow = uow;
            _context = context;
            _mapper=mapper;
        }

        public async Task<IResponse<List<Variety>>> GetProductVarities(int productId)
        {
            var list=await _context.Varieties.Where(i=>i.ProductId==productId).ToListAsync();
            return new Response<List<Variety>>(ResponseType.Success,list);
        }

        public async Task<IResponse<List<BasicVarietyDTO>>> GetSellerVarieties(int? subCategoryid, int? subSubCategoryid)
        {
            var specs = new List<Variety>();
            if (subCategoryid != null)
                specs = await _context.Varieties.Where(i => i.SubCategoryId == subCategoryid).ToListAsync();
            if (subSubCategoryid != null)
                specs = await _context.Varieties.Where(i => i.SubSubCategoryId == subSubCategoryid).ToListAsync();
            var mapped = _mapper.Map<List<BasicVarietyDTO>>(specs);
            return new Response<List<BasicVarietyDTO>>(ResponseType.Success, mapped);
        }

        public async Task<IResponse<List<Variety>>> GetVarietiesForSearch(int? subcatid, int? subsubcatid)
        {
            var list = await _context.Varieties.ToListAsync();

            if (subcatid != null)
                list = list.Where(i => i.SubCategoryId == subcatid).ToList();
            if (subsubcatid != null)
                list = list.Where(i => i.SubSubCategoryId == subsubcatid).ToList();
            list = list.OrderBy(i => i.VarietyName).ToList();
            return new Response<List<Variety>>(ResponseType.Success, list);
        }
    }
}
