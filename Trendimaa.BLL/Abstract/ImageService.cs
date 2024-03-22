using FluentValidation;
using Trendeimaa.Entities;
using Trendimaa.BLL.Interface;
using Trendimaa.DAL.Context;
using Trendimaa.DAL.UnitOfWork;

namespace Trendimaa.BLL.Abstract
{
    public class ImageService : Service<Image>, IImageService
    {
        public ImageService(IValidator<Image> validator, IUOW uow, TrendimaaContext context) : base(validator, uow, context)
        {
        }
    }
}
