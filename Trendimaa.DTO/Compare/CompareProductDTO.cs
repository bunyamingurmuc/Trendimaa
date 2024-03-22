using Trendimaa.Common.Enum;
using Trendimaa.DTO.Compare;

namespace Trendimaa.DTO.Product
{
    public class CompareProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Detail { get; set; }
        public int StockPrice { get; set; }
        public double? FreeShippingLimitPrice { get; set; } = 0;
        public double ShippingPrice { get; set; }
        public string Description { get; set; }
        public Language Language { get; set; }
        public Currency Currency { get; set; }

        public string SellerName { get; set; }
        public string SellerImageUrl { get; set; }
        public List<ImageDTO> Images { get; set; }
        public List<CompareSpecificationDTO> Specifications { get; set; }

    }
}
