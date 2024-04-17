using Trendeimaa.Entities;
using Trendimaa.Common;

namespace Trendimaa.BLL.Interface
{
    public interface ISpecificationService : IService<Specification>
    {
        Task<IResponse<List<Specification>>> GetProductSpecifications(int productId);
        Task<IResponse<List<Specification>>> GetSpecificationsForSearch( int? subcatid, int? subsubcatid);
    }
}
