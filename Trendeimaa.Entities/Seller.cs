using Trendeimaa.Entities.Interface;
using Trendimaa.Common.Enum;

namespace Trendeimaa.Entities
{
    public class Seller:BaseEntity
    {
        public Seller()
        {
            Products = new List<Product>();
            QuestionAnswers = new List<QuestionAnswer>();
            Coupons = new List<Coupon>();
            Orders = new List<Order>();
        }
        public string CompanyName { get; set; }
        public string PhoneNumber { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public int TaxIdentificationNumber { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string IBAN { get; set; }

        public int? AddressId { get; set; }
        public Address? Address { get; set; }

        public Language Language { get; set; }
        public Currency Currency { get; set; }
        public List<Product> Products{ get; set; }
        public List<QuestionAnswer> QuestionAnswers { get; set; }
        public List<Coupon> Coupons { get; set; }
        public List<Order> Orders { get; set; }
        public int? ImageId { get; set; }
        public Image Image { get; set; }

    }
}
