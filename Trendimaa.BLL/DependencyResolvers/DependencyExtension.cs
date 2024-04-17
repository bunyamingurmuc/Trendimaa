using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Trendeimaa.Entities;
using Trendeimaa.Entities.CategoryFolder;
using Trendimaa.BLL.Abstract;
using Trendimaa.BLL.Interface;
using Trendimaa.BLL.ValidationRules;
using Trendimaa.DAL.Context;
using Trendimaa.DAL.UnitOfWork;

namespace Trendimaa.BLL.DependencyResolvers
{
    public static class DependencyExtension
    {
        public static void AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {



            services.AddDbContext<TrendimaaContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("Local"));
            });
            services.AddScoped<IUOW, UOW>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IAppUserService, AppUserService>();
            services.AddScoped<ISpecificationService, SpecificationService>();
            services.AddScoped<IVarietyService, VarietyService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ISubCategoryService, SubCategoryService>();
            services.AddScoped<ISubSubCategoryService, SubSubCategoryService>();
            services.AddScoped<ISellerService, SellerService>();
            services.AddScoped<IQuestionService, QuestionService>();
            services.AddScoped<IAnswerService, AnswerService>();
            services.AddScoped<ICouponService, CouponService>();
            services.AddScoped<ICardService, CardService>();
            services.AddScoped<ICouponService, CouponService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<ICardItemService, CardItemService>();
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<ISearchRelatedService, SearchRelatedService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IFavoriteService, FavoriteService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<ICampaignService, CampaignService>();
            services.AddScoped<IWalletService, WalletService>();
            services.AddScoped<IWalletItemService, WalletItemService>();


            services.AddSingleton<IValidator<AppUser>, AppUserValidator>();
            services.AddSingleton<IValidator<Product>, ProductValidator>();
            services.AddSingleton<IValidator<Variety>, VarietyValidator>();
            services.AddSingleton<IValidator<Variety>, VarietyValidator>();
            services.AddSingleton<IValidator<Specification>, SpecificationValidator>();
            services.AddSingleton<IValidator<Category>, CategoryValidator>();
            services.AddSingleton<IValidator<SubCategory>, SubCategoryValidator>();
            services.AddSingleton<IValidator<SubSubCategory>, SubSubCategoryValidator>();
            services.AddSingleton<IValidator<Question>, QuestionValidator>();
            services.AddSingleton<IValidator<Answer>, AnswerValidator>();
            services.AddSingleton<IValidator<Seller>, SellerValidator>();
            services.AddSingleton<IValidator<Comment>, CommentValidator>();
            services.AddSingleton<IValidator<Coupon>, CouponValidator>();
            services.AddSingleton<IValidator<Card>, CardValidator>();
            services.AddSingleton<IValidator<CardItem>, CardItemValidator>();
            services.AddSingleton<IValidator<Address>, AddressValidator>();
            services.AddSingleton<IValidator<SearchRelated>, SearchRelatedValidator>();
            services.AddSingleton<IValidator<Notification>, NotificationValidator>();
            services.AddSingleton<IValidator<Favorite>, FavoriteValidator>();
            services.AddSingleton<IValidator<Image>, ImageValidator>();
            services.AddSingleton<IValidator<Campaign>, CampaignValidator>();
            services.AddSingleton<IValidator<Wallet>, WalletValidator>();
            services.AddSingleton<IValidator<WalletItem>, WalletItemValidator>();


        }
    }
}
