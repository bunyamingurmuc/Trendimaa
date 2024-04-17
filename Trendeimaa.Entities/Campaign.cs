using Trendeimaa.Entities.Interface;
using Trendimaa.Common.Enum;

namespace Trendeimaa.Entities
{
    public class Campaign:BaseEntity
    {
        public Campaign()
        {
            Products= new List<Product>();
            Images= new List<Image>();
        }   
        public string Name { get; set; }
        public string? UpperTitle { get; set; }
        public string? MiddleTitle { get; set; }
        public string? LowerTitle { get; set; }
        public string? BoldLowerTitle { get; set; }
        public string? Description { get; set; }
        public string? Detail { get; set; }
        public BannerSection BannerSection{ get; set; }
        public bool? IsHome { get; set; }
        public bool IsForMobile{ get; set; }
        public Language Language{ get; set; }
        public DateTime ExpireDate { get; set; }
        public List<Product> Products { get; set; }
        public List<Image>Images { get; set; }
    }
}
