namespace Trendimaa.DTO.Seller
{
    public class SellerCardDTO
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public int? ImageId { get; set; }
        public ImageDTO Image { get; set; }
    }
}
