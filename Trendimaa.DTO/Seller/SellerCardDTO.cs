using Trendeimaa.Entities.Interface;

namespace Trendimaa.DTO.Seller
{
    public class SellerCardDTO:BaseEntity
    {
        public string CompanyName { get; set; }
        public int? ImageId { get; set; }
        public double? rate { get; set; } = 0;
        public ImageDTO Image { get; set; }
    }
}
