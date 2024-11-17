using Microsoft.EntityFrameworkCore;
using Orde.Entity.RelatedTables;
using Trendeimaa.Entities;
using Trendeimaa.Entities.CategoryFolder;
using Trendeimaa.Entities.Related;

namespace Trendimaa.DAL.Relations
{
    public class ApplyRelations
    {
        public static void ApplyRelation(ModelBuilder builder)
        {
            builder.Entity<Product>().HasMany(i=>i.Images).WithOne(i=>i.Product).HasForeignKey(i=>i.ProductId).IsRequired(false).OnDelete(DeleteBehavior.SetNull);
            builder.Entity<Product>().HasMany(i=>i.Specifications).WithOne(i=>i.Product).HasForeignKey(i=>i.ProductId).IsRequired(false).OnDelete(DeleteBehavior.SetNull);
            builder.Entity<Product>().HasMany(i=>i.Varieties).WithOne(i=>i.Product).HasForeignKey(i=>i.ProductId).IsRequired(false).OnDelete(DeleteBehavior.SetNull);
            builder.Entity<Category>().HasMany(i=>i.Products).WithOne(i=>i.Category).HasForeignKey(i=>i.CategoryId).IsRequired(false).OnDelete(DeleteBehavior.SetNull);
            builder.Entity<Seller>().HasMany(i=>i.Products).WithOne(i=>i.Seller).HasForeignKey(i=>i.SellerId).IsRequired(false).OnDelete(DeleteBehavior.SetNull);
           
            builder.Entity<Comment>().HasOne(i=>i.Product).WithMany(i=>i.Comments).HasForeignKey(i=>i.ProductId).IsRequired(false).OnDelete(DeleteBehavior.SetNull);
            builder.Entity<Comment>().HasMany(i=>i.Images).WithOne(i=>i.Comment).HasForeignKey(i=>i.CommentId).IsRequired(false).OnDelete(DeleteBehavior.SetNull);
            builder.Entity<Comment>().HasMany(i=>i.Images).WithOne(i=>i.Comment).HasForeignKey(i=>i.CommentId).IsRequired(false).OnDelete(DeleteBehavior.SetNull);
            builder.Entity<Comment>().HasOne(i=>i.AppUser).WithMany(i=>i.Comments).HasForeignKey(i=>i.AppUserId).IsRequired(false).OnDelete(DeleteBehavior.SetNull);
            builder.Entity<Coupon>().HasOne(i=>i.Seller).WithMany(i=>i.Coupons).HasForeignKey(i=>i.SellerId).IsRequired(false).OnDelete(DeleteBehavior.SetNull);
            builder.Entity<Coupon>().HasOne(i=>i.AppUser).WithMany(i=>i.Coupons).HasForeignKey(i=>i.AppUserId).IsRequired(false).OnDelete(DeleteBehavior.SetNull);
            builder.Entity<Card>().HasOne(i=>i.AppUser).WithMany(i=>i.Carts).HasForeignKey(i=>i.AppUserId).IsRequired(false).OnDelete(DeleteBehavior.SetNull);
            builder.Entity<Card>().HasMany(i=>i.CardItems).WithOne(i=>i.Card).HasForeignKey(i=>i.CardId).IsRequired(false).OnDelete(DeleteBehavior.SetNull);
            builder.Entity<CardItem>().HasOne(i=>i.Product).WithMany(i=>i.CardItems).HasForeignKey(i=>i.ProductId).IsRequired(false).OnDelete(DeleteBehavior.SetNull);
            builder.Entity<CardItem>().HasOne(i=>i.Card).WithMany(i=>i.CardItems).HasForeignKey(i=>i.CardId).IsRequired(false).OnDelete(DeleteBehavior.SetNull);
            builder.Entity<Address>().HasOne(a => a.Seller).WithOne(s => s.Address).HasForeignKey<Seller>(s => s.AddressId).IsRequired(false).OnDelete(DeleteBehavior.SetNull);
            builder.Entity<Address>().HasOne(a => a.AppUser).WithMany(s => s.Addresses).HasForeignKey(s => s.AppUserId).IsRequired(false).OnDelete(DeleteBehavior.SetNull);
            builder.Entity<Campaign>().HasMany(a => a.Images).WithOne(s => s.Campaign).HasForeignKey(s => s.CampaignId).IsRequired(false).OnDelete(DeleteBehavior.SetNull);
            
            builder.Entity<SearchRelated>().HasOne(a => a.Product).WithMany(s => s.SearchRelateds).HasForeignKey(s => s.ProductId).IsRequired(false).OnDelete(DeleteBehavior.SetNull);
            builder.Entity<SearchRelated>().HasOne(a => a.Category).WithMany(s => s.SearchRelateds).HasForeignKey(s => s.CategoryId).IsRequired(false).OnDelete(DeleteBehavior.SetNull);
            builder.Entity<Category>().HasMany(a => a.Images).WithOne(s => s.Category).HasForeignKey(s => s.CategoryId).IsRequired(false).OnDelete(DeleteBehavior.SetNull);
            builder.Entity<SearchRelated>().HasOne(a => a.AppUser).WithMany(s => s.SearchRelateds).HasForeignKey(s => s.AppUserId).IsRequired(false).OnDelete(DeleteBehavior.SetNull);
           
            builder.Entity<Category>().HasMany(a => a.SubCategories).WithOne(s => s.Category).HasForeignKey(s => s.CategoryId).IsRequired(false).OnDelete(DeleteBehavior.SetNull);
            
            builder.Entity<SubCategory>().HasMany(a => a.SubSubCategories).WithOne(s => s.SubCategory).HasForeignKey(s => s.SubCategoryId).IsRequired(false).OnDelete(DeleteBehavior.SetNull);
            
            builder.Entity<SubCategory>().HasMany(a => a.Products).WithOne(s => s.SubCategory).HasForeignKey(s => s.SubCategoryId).IsRequired(false).OnDelete(DeleteBehavior.SetNull);
            builder.Entity<SubCategory>().HasMany(a => a.SearchRelateds).WithOne(s => s.SubCategory).HasForeignKey(s => s.SubCategoryId).IsRequired(false).OnDelete(DeleteBehavior.SetNull);
            builder.Entity<SubCategory>().HasMany(a => a.Specifications).WithOne(s => s.SubCategory).HasForeignKey(s => s.SubCategoryId).IsRequired(false).OnDelete(DeleteBehavior.SetNull);
            builder.Entity<SubCategory>().HasMany(a => a.Varieties).WithOne(s => s.SubCategory).HasForeignKey(s => s.SubCategoryId).IsRequired(false).OnDelete(DeleteBehavior.SetNull);

            builder.Entity<SubSubCategory>().HasMany(a => a.Products).WithOne(s => s.SubSubCategory).HasForeignKey(s => s.SubSubCategoryId).IsRequired(false).OnDelete(DeleteBehavior.SetNull);
            builder.Entity<SubSubCategory>().HasMany(a => a.SearchRelateds).WithOne(s => s.SubSubCategory).HasForeignKey(s => s.SubSubCategoryId).IsRequired(false).OnDelete(DeleteBehavior.SetNull);
            builder.Entity<SubSubCategory>().HasMany(a => a.Specifications).WithOne(s => s.SubSubCategory).HasForeignKey(s => s.SubSubCategoryId).IsRequired(false).OnDelete(DeleteBehavior.SetNull);
            builder.Entity<SubSubCategory>().HasMany(a => a.Varieties).WithOne(s => s.SubSubCategory).HasForeignKey(s => s.SubSubCategoryId).IsRequired(false).OnDelete(DeleteBehavior.SetNull);
            
            builder.Entity<Order>().HasOne(a => a.AppUser).WithMany(s => s.Orders).HasForeignKey(s => s.AppUserId).IsRequired(false).OnDelete(DeleteBehavior.SetNull);
            builder.Entity<Order>().HasOne(a => a.Seller).WithMany(s => s.Orders).HasForeignKey(s => s.SellerId).IsRequired(false).OnDelete(DeleteBehavior.SetNull);
            builder.Entity<Order>().HasOne(a => a.Coupon).WithOne(s => s.Order).HasForeignKey<Order>(s => s.CouponId).IsRequired(false).OnDelete(DeleteBehavior.SetNull);
            builder.Entity<Order>().HasMany(a => a.OrderItems).WithOne(s => s.Order).HasForeignKey(s => s.OrderId).IsRequired(false).OnDelete(DeleteBehavior.SetNull);
            
            builder.Entity<OrderItem>().HasOne(a => a.Order).WithMany(s => s.OrderItems).HasForeignKey(s => s.OrderId).IsRequired(false).OnDelete(DeleteBehavior.SetNull);
            builder.Entity<OrderItem>().HasOne(a => a.Product).WithMany(s => s.OrderItems).HasForeignKey(s => s.ProductId).IsRequired(false).OnDelete(DeleteBehavior.SetNull);
            
            builder.Entity<Seller>().HasOne(a => a.Image).WithOne(s => s.Seller).HasForeignKey<Image>(s => s.SellerId).IsRequired(false).OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Notification>().HasOne(a => a.Image).WithOne(s => s.Notification).HasForeignKey<Notification>(s => s.ImageId).IsRequired(false).OnDelete(DeleteBehavior.SetNull);

            builder.Entity<FavoriteProduct>().HasKey(sc => new { sc.FavoriteId, sc.ProductId});
            
            builder.Entity<Favorite>().HasOne(a => a.AppUser).WithMany(s => s.Favorites).HasForeignKey(s => s.AppUserId).IsRequired(false).OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Wallet>().HasOne(a => a.AppUser).WithOne(s => s.Wallet).HasForeignKey<Wallet>(s => s.AppUserId).IsRequired(false).OnDelete(DeleteBehavior.SetNull);
            builder.Entity<Wallet>().HasMany(a => a.WalletItems).WithOne(s => s.Wallet).HasForeignKey(s => s.WalletId).IsRequired(false).OnDelete(DeleteBehavior.SetNull);
            builder.Entity<WalletItem>().HasOne(a => a.Order).WithMany(s => s.WalletItems).HasForeignKey(s => s.OrderId).IsRequired(false).OnDelete(DeleteBehavior.SetNull);
            builder.Entity<WalletItem>().HasOne(a => a.Wallet).WithMany(s => s.WalletItems).HasForeignKey(s => s.WalletId).IsRequired(false).OnDelete(DeleteBehavior.SetNull);
            
            builder.Entity<Question>().HasOne(a => a.Product).WithMany(s => s.Questions).HasForeignKey(s => s.ProductId).IsRequired(false).OnDelete(DeleteBehavior.SetNull);
            builder.Entity<Question>().HasOne(a => a.AppUser).WithMany(s => s.Questions).HasForeignKey(s => s.AppUserId).IsRequired(false).OnDelete(DeleteBehavior.SetNull);
            builder.Entity<Question>().HasOne(a => a.Answer).WithOne(s => s.Question).HasForeignKey<Answer>(s => s.QuestionId).IsRequired(false).OnDelete(DeleteBehavior.SetNull);
           
            builder.Entity<Answer>().HasOne(a => a.Seller).WithMany(s => s.Answers).HasForeignKey(s => s.SellerId).IsRequired(false).OnDelete(DeleteBehavior.SetNull);
            builder.Entity<Answer>().HasOne(a => a.Question).WithOne(s => s.Answer).HasForeignKey<Question>(s => s.AnswerId).IsRequired(false).OnDelete(DeleteBehavior.SetNull);
            builder.Entity<CreditCard>().HasOne(a => a.AppUser).WithMany(s => s.CreditCards).HasForeignKey(s => s.AppUserId).IsRequired(false).OnDelete(DeleteBehavior.SetNull);

            builder.Entity<CouponOffer>().HasMany(i => i.Coupons).WithOne(i => i.CouponOffer).HasForeignKey(s => s.CouponOfferId).IsRequired(false).OnDelete(DeleteBehavior.SetNull);
            builder.Entity<SellerCouponOffer>().HasKey(sc => new { sc.CouponOfferId, sc.SellerId });
            builder.Entity<SellerCoupon>().HasKey(sc => new { sc.SellerId, sc.CouponId });
           
            
            builder.Entity<UserKey>().HasOne(i => i.AppUser).WithMany(i => i.UserKeys).HasForeignKey(s => s.AppUserId).IsRequired(false).OnDelete(DeleteBehavior.SetNull);
            builder.Entity<UserKey>().HasOne(i => i.Seller).WithMany(i => i.UserKeys).HasForeignKey(s => s.SellerId).IsRequired(false).OnDelete(DeleteBehavior.SetNull);

           builder.Entity<Notification>().HasOne(i => i.AppUser).WithMany(i => i.Notifications).HasForeignKey(s => s.AppUserId).IsRequired(false).OnDelete(DeleteBehavior.SetNull);
            builder.Entity<Notification>().HasOne(i => i.Seller).WithMany(i => i.Notifications).HasForeignKey(s => s.SellerId).IsRequired(false).OnDelete(DeleteBehavior.SetNull);
            
            builder.Entity<AlarmItem>().HasOne(i => i.Product).WithMany(i => i.AlarmItems).HasForeignKey(s => s.ProductId).IsRequired(false).OnDelete(DeleteBehavior.SetNull);
            builder.Entity<AlarmItem>().HasOne(i => i.AppUser).WithMany(i => i.AlarmItems).HasForeignKey(s => s.AppUserId).IsRequired(false).OnDelete(DeleteBehavior.SetNull);

        }
    }
}
