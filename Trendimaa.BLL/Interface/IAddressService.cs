using Trendeimaa.Entities;
using Trendimaa.Common;

namespace Trendimaa.BLL.Interface
{
    public interface IAddressService:IService<Address>
    {
        Task<IResponse<List<Address>>> GetUserAddresses(int id);
    }
}
