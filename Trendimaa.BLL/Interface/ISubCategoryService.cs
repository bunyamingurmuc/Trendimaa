using Trendeimaa.Entities;
using Trendeimaa.Entities.CategoryFolder;
using Trendimaa.Common;
using Trendimaa.Common.Enum;
using Trendimaa.DTO.CategoryDTOs;

namespace Trendimaa.BLL.Interface
{
    public interface ISubCategoryService:IService<SubCategory>
    {
       public Task<IResponse<List<CategoryHomeDTO>>> GetSubCategories(Language language, int catId);

    }
}
