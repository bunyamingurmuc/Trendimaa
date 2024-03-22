using Trendeimaa.Entities;
using Trendimaa.Common;
using Trendimaa.Common.Enum;
using Trendimaa.DTO.Image;

namespace Trendimaa.BLL.Interface
{
    public interface ICampaignService:IService<Campaign>
    {
        Task<IResponse<List<MainHomeCampaignDTO>>> GetMainHomeCampaigns(Language language);
    }
}
