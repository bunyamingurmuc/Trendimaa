using Trendeimaa.Entities;
using Trendimaa.Common;

namespace Trendimaa.BLL.Interface
{
    public interface ICardService:IService<Card>
    {
        Task<IResponse<Card>> GetUserCard(int appuserId);

    }
}
