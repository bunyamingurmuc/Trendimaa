using Trendimaa.Common.Enum;

namespace Trendimaa.DTO.Image
{
    public class MainHomeCampaignDTO
    {
        public int? ImageId { get; set; }
        public ImageDTO? Image { get; set; }
        public string? MiddleTitle { get; set; }
        public string? LowerTitle { get; set; }
        public string? BoldLowerTitle { get; set; }
        public string? Description { get; set; }
        public string? Detail { get; set; }
        public BannerSection BannerSection { get; set; }
    }
}
