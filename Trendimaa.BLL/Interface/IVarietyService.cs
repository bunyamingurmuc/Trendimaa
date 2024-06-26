using Trendeimaa.Entities;
using Trendimaa.Common;
using Trendimaa.DTO;

namespace Trendimaa.BLL.Interface
{
    public interface IVarietyService:IService<Variety>
    {
        Task<IResponse<List<Variety>>> GetProductVarities(int productId);
        Task<IResponse<List<BasicVarietyDTO>>> GetSellerVarieties(int? subCategoryid, int? subSubCategoryid);
    }
}
