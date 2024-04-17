using Trendimaa.DTO.Product;

namespace Trendimaa.DTO.Listing
{
    public class CSellerProductsDto
    {
        public int TotalProductCount { get; set; }
        public List<ProductSellerListDto> Products { get; set; }
    }


}
