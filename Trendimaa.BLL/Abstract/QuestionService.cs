using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Trendeimaa.Entities;
using Trendimaa.BLL.Interface;
using Trendimaa.Common;
using Trendimaa.DAL.Context;
using Trendimaa.DAL.UnitOfWork;
using Trendimaa.DTO;
using Trendimaa.DTO.SearchDTOs;

namespace Trendimaa.BLL.Abstract
{
    public class QuestionService : Service<Question>, IQuestionService
    {
        public readonly IMapper _mapper;
        public readonly TrendimaaContext _context;
        public readonly IValidator<Question> _validator;
        public readonly IUOW _uow;
        public QuestionService(IValidator<Question> validator, IUOW uow, IMapper mapper, TrendimaaContext context) : base(validator, uow, context)
        {
            _context = context;
            _mapper = mapper;
            _uow = uow;
            _validator = validator;
        }

       public async Task<IResponse<List<QuestionDTO>>> GetProductQuestions(int? productId)
        {
            var list = await _context.Questions.Where(i=>i.ProductId==productId).Include(i => i.Answer).AsNoTracking().ToListAsync();
            var mapped = _mapper.Map<List<QuestionDTO>>(list);
            return new Response<List<QuestionDTO>>(ResponseType.Success, mapped);
        }

       
    }
}
