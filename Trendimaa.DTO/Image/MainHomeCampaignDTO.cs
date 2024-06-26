using Trendimaa.Common.Enum;

namespace Trendimaa.DTO.Image
{
    public class MainHomeCampaignDTO
    {
        public List<ImageDTO> Images{ get; set; }
        public string? MiddleTitle { get; set; }
        public string? LowerTitle { get; set; }
        public string? BoldLowerTitle { get; set; }
        public string? Description { get; set; }
        public string? Detail { get; set; }
        public BannerSection BannerSection { get; set; }
    }
}
