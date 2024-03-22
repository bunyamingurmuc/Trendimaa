using System.Reflection.Metadata.Ecma335;
using Trendeimaa.Entities.Interface;

namespace Trendeimaa.Entities
{
    public class Coupon:BaseEntity
    {
        public double Price { get; set; }
        public DateTime ExpireTime { get; set; }
        public int? SellerId { get; set; }
        public Seller? Seller { get; set; }
        public int? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }  
        public int? OrderId { get; set; }
        public Order? Order { get; set; }
    }
}
