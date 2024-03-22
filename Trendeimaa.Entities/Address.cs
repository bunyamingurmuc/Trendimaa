using Trendeimaa.Entities.Interface;

namespace Trendeimaa.Entities
{
    public class Address:BaseEntity
    {
        public string AddressTopic { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Suburb { get; set; }
        public int PostalCode { get; set; }
        public int? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        public int? SellerId { get; set; }
        public Seller? Seller { get; set; }
    }
}
