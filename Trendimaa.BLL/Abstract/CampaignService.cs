using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Trendeimaa.Entities;
using Trendimaa.BLL.Interface;
using Trendimaa.Common;
using Trendimaa.Common.Enum;
using Trendimaa.DAL.Context;
using Trendimaa.DAL.UnitOfWork;
using Trendimaa.DTO.Image;

namespace Trendimaa.BLL.Abstract
{
    public class CampaignService : Service<Campaign>,ICampaignService
    {
        public readonly TrendimaaContext _context;
        public readonly IValidator<Campaign> _validator;
        private readonly IUOW _uow;
        private readonly IMapper _mapper;

        public CampaignService(IValidator<Campaign> validator, IMapper mapper, IUOW uow, TrendimaaContext context) : base(validator, uow, context)
        {
            _validator = validator;
            _context = context;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<IResponse<List<MainHomeCampaignDTO>>> GetMainHomeCampaigns(Language language)
        {
            var campanies=await _context.Campaigns
                   .Where(i => i.IsHome == true && i.ExpireDate >= DateTime.Now)
                   .Where(i=>i.Language==language)
                   .Include(i=>i.Images)
                   .ToListAsync();
                var mapped=_mapper.Map<List<MainHomeCampaignDTO>>(campanies);
            return new Response<List<MainHomeCampaignDTO>>(ResponseType.Success,mapped);

        }
    }
}
