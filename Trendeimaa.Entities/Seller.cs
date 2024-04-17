using Trendeimaa.Entities.Interface;
using Trendimaa.Common.Enum;

namespace Trendeimaa.Entities
{
    public class Seller:BaseEntity
    {
        public Seller()
        {
            Products = new List<Product>();
            Coupons = new List<Coupon>();
            Orders = new List<Order>();
            Answers = new List<Answer>();
        }
        public string CompanyName { get; set; }
        public string CompanyFullName { get; set; }
        public string PhoneNumber { get; set; }
        public string EMail { get; set; }
        public string Password { get; set; }
        public int TaxIdentificationNumber { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string IBAN { get; set; }
        public double? rate { get; set; } = 0;
        public bool? IsActive{ get; set; }=null;

        public int? AddressId { get; set; }
        public Address? Address { get; set; }

        public Language Language { get; set; }
        public Currency Currency { get; set; }
        public List<Product> Products{ get; set; }
        public List<Coupon> Coupons { get; set; }
        public List<Order> Orders { get; set; }
        public List<Answer> Answers { get; set; }
        public int? ImageId { get; set; }
        public Image? Image { get; set; }

    }
}
