using Trendeimaa.Entities.Interface;

namespace Trendimaa.DTO.Product
{
    public class ProductSellerListDto:BaseEntity
    {
        public string Name { get; set; }
        public string Brand { get; set; }
        public int StockPiece { get; set; }
        public int StockPrice { get; set; }
        public string? StockCode { get; set; }
        public double Rate { get; set; }
        public List<ImageDTO>Images{ get; set; }
    }
}
