using Trendeimaa.Entities;
using Trendeimaa.Entities.CategoryFolder;
using Trendimaa.Common.Enum;
using Trendimaa.Common;
using Trendimaa.DTO.CategoryDTOs;

namespace Trendimaa.BLL.Interface
{
    public interface ISubSubCategoryService:IService<SubSubCategory>
    {

        public Task<IResponse<List<CategoryHomeDTO>>> GetSubSubCategories(Language language, int subCatId);

    }

}
