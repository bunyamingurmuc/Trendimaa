using Microsoft.EntityFrameworkCore;
using Trendeimaa.Entities;
using Trendeimaa.Entities.CategoryFolder;
using Trendimaa.DAL.Relations;

namespace Trendimaa.DAL.Context
{
    public class TrendimaaContext: DbContext
    {
        public TrendimaaContext(DbContextOptions<TrendimaaContext> options) : base(options)
        { 
        }
        public DbSet<Product>? Products { get; set; }
        public DbSet<Image>? Images{ get; set; }
        public DbSet<AppUser>? AppUsers{ get; set; }
        public DbSet<Specification>? Specifications{ get; set; }
        public DbSet<Variety>? Varieties{ get; set; }
        public DbSet<Category>? Categories{ get; set; }
        public DbSet<SubCategory>? SubCategoies { get; set; }
        public DbSet<SubSubCategory>? SubSubCategories{ get; set; }
        public DbSet<Seller>? Sellers{ get; set; }
        public DbSet<Comment>? Comments{ get; set; }
        public DbSet<Question>? Questions{ get; set; }
        public DbSet<Answer>? Answers{ get; set; }
        public DbSet<Campaign>? Campaigns{ get; set; }
        public DbSet<Coupon>? Coupons{ get; set; }
        public DbSet<CouponOffer>? CouponOffers{ get; set; }
        public DbSet<Card>? Cards { get; set; }
        public DbSet<CardItem>? CardItems { get; set; }
        public DbSet<SearchRelated>? SearchRelateds { get; set; }
        public DbSet<Notification>? Notifications{ get; set; }
        public DbSet<Favorite>? Favorites{ get; set; }
        public DbSet<Wallet>? Wallets{ get; set; }
        public DbSet<WalletItem>? WalletItems { get; set; }
        public DbSet<CreditCard>? CreditCards { get; set; }
        public DbSet<Address>? Addresses { get; set; }
        public DbSet<UserKey>? UserKeys { get; set; }
        public DbSet<Order>? Orders { get; set; }
        public DbSet<OrderItem>? OrderItems { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {


            //AutoIncludes.AutoInclude(builder);
            ApplyRelations.ApplyRelation(builder);
            
            base.OnModelCreating(builder);

        }


    }
}
