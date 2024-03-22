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
        public bool IsMainHomeCarousel { get; set; }
        public bool IsMainHomeSpace { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Detail { get; set; }
        public bool IsHome { get; set; }
        public Language Language{ get; set; }
        public DateTime ExpireDate { get; set; }
        public List<Product> Products { get; set; }
        public List<Image>Images { get; set; }
    }
}
