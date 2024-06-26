using Trendeimaa.Entities.Interface;
using Trendimaa.DTO.Seller;

namespace Trendimaa.DTO.Product
{
    public class ProductSellerDTO:BaseEntity
    {
        public string Name { get; set; }
        public string Brand { get; set; }
        public double StockPrice { get; set; }
        public int? Discount { get; set; } = 0;
        public int? SellerId { get; set; }
        public SellerCardDTO? Seller { get; set; }
        public List<ImageDTO> Images { get; set; }

    }
}
