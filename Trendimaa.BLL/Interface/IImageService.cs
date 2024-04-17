using Trendeimaa.Entities;

namespace Trendimaa.BLL.Interface
{
    public interface IImageService:IService<Image>
    {
        public void DeleteAll();
    }
}
