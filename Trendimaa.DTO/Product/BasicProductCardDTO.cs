namespace Trendimaa.DTO.Product
{
    public class BasicProductCardDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public double Rate { get; set; }
        public int StockPrice { get; set; }
        public double MyProperty { get; set; }
        public int ImageId { get; set; }
        public ImageDTO Image { get; set; }

    }
}
