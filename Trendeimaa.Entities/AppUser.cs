using Trendeimaa.Entities.Interface;

namespace Trendeimaa.Entities
{
    public class AppUser:BaseEntity
    {

        public AppUser()
        {
            Comments = new List<Comment>();
            QuestionAnswers = new List<QuestionAnswer>();
            Coupons = new List<Coupon>();
            Carts = new List<Card>();
            Addresses = new List<Address>();
            SearchRelateds = new List<SearchRelated>();
            Orders = new List<Order> ();
            Favorites =new List<Favorite> ();

        }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber{ get; set; }
        public string UserIdentity { get; set; }
        public List<Comment>Comments { get; set; }
        public List<QuestionAnswer> QuestionAnswers { get; set; }
        public List<Coupon> Coupons { get; set; }
        public List<Card> Carts { get; set; }
        public List<Address> Addresses { get; set; }
        public List<SearchRelated> SearchRelateds { get; set; }
        public List<Order> Orders{ get; set; }
        public List<Favorite> Favorites{ get; set; }

    }
}
