using Trendimaa.Common.Enum;

namespace Trendimaa.DTO.Product
{
    public class VarietiesPriceDto
    {
        public VarietyName VarietyName { get; set; }
        public double StockPrice { get; set; }
        public bool? IsStock { get; set; }
        public int? Id { get; set; }

    }
}
