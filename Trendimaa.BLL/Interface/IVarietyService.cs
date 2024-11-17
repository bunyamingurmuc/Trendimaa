using Trendeimaa.Entities;
using Trendimaa.Common;
using Trendimaa.DTO;
using Trendimaa.DTO.Product;

namespace Trendimaa.BLL.Interface
{
    public interface IVarietyService:IService<Variety>
    {
        Task<IResponse<List<Variety>>> GetProductVarities(int productId);
        Task<IResponse<List<BasicVarietyDTO>>> GetSellerVarieties(int? subCategoryid, int? subSubCategoryid);
        Task<IResponse<List<Variety>>> GetVarietiesForSearch(int? subcatid, int? subsubcatid);
       

    }
}
