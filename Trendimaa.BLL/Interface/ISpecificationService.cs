using Trendeimaa.Entities;
using Trendimaa.Common;
using Trendimaa.DTO;

namespace Trendimaa.BLL.Interface
{
    public interface ISpecificationService : IService<Specification>
    {
        Task<IResponse<List<Specification>>> GetProductSpecifications(int productId);
        Task<IResponse<List<Specification>>> GetSpecificationsForSearch( int? subcatid, int? subsubcatid);
        Task<IResponse<List<BasicSpecificationDTO>>> GetSellerSpecifications(int? subCategoryid,int? subSubCategoryid);
    }
}
