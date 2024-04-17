using Trendeimaa.Entities;
using Trendimaa.Common;

namespace Trendimaa.BLL.Interface
{
    public interface IVarietyService:IService<Variety>
    {
        Task<IResponse<List<Variety>>> GetProductVarities(int productId);
    }
}
