using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Trendeimaa.Entities;
using Trendimaa.BLL.Interface;
using Trendimaa.DAL.Context;
using Trendimaa.DAL.UnitOfWork;

namespace Trendimaa.BLL.Abstract
{
    public class ImageService : Service<Image>, IImageService
    {
        public readonly TrendimaaContext _context;
        public readonly IValidator<Image> _validator;
        private readonly IUOW _uow;
        private readonly IMapper _mapper;
        public ImageService(IValidator<Image> validator,IMapper mapper, IUOW uow, TrendimaaContext context) : base(validator, uow, context)
        {
            _context = context;
            _validator = validator;
            _mapper = mapper;
            _uow = uow;
        }

        public async void DeleteAll()
        {
            var images = _context.Images.ToList();
            _uow.GetRepository<Image>().RemoveRange(images);
        }
    }
}
