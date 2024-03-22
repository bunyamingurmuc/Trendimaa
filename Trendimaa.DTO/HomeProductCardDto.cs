using Trendimaa.Common.Enum;

namespace Trendimaa.DTO
{
    public class HomeProductCardDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Detail { get; set; }
        public int StockPrice { get; set; }
        public Currency Currency { get; set; }
        public List<ImageDTO> Images { get; set; }
    }
}
