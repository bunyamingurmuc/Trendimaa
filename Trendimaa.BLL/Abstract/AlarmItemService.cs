using AutoMapper;
using Azure;
using FluentValidation;
using Trendeimaa.Entities;
using Trendimaa.BLL.Interface;
using Trendimaa.DAL.Context;
using Trendimaa.DAL.UnitOfWork;

namespace Trendimaa.BLL.Abstract
{
    public class AlarmItemService : Service<AlarmItem>, IAlarmItemService
    {
        public readonly IValidator<AlarmItem> _validator;
        public readonly IUOW _uow;
        public readonly TrendimaaContext _context;
        public readonly IMapper _mapper;

        public AlarmItemService(IValidator<AlarmItem> validator, IUOW uow, TrendimaaContext context,IMapper mapper) : base(validator, uow, context)
        {
            _validator = validator;
            _uow = uow;
            _context = context;
            _mapper = mapper;

        }

        
    }
}
