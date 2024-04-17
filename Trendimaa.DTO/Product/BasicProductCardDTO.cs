using Trendeimaa.Entities.Interface;

namespace Trendimaa.DTO.Product
{
    public class BasicProductCardDTO:BaseEntity
    {
 
        public string Name { get; set; }
        public string Detail { get; set; }
        public double Rate { get; set; }
        public int StockPrice { get; set; }
        public int? Discount { get; set; }
        public int? CashbackAmount{ get; set; }
        public List<ImageDTO> Images { get; set; }

    }
}
