using Trendeimaa.Entities.Interface;
using Trendimaa.Common.Enum;

namespace Trendeimaa.Entities
{
    public class Address:BaseEntity
    {
        public string AddressTopic { get; set; }
        public AddressType AddressType { get; set; }
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
