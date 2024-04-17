using AutoMapper;
using Trendeimaa.Entities;
using Trendeimaa.Entities.CategoryFolder;
using Trendimaa.BLL.Abstract;
using Trendimaa.DTO;
using Trendimaa.DTO.CategoryDTOs;
using Trendimaa.DTO.Compare;
using Trendimaa.DTO.Image;
using Trendimaa.DTO.Order;
using Trendimaa.DTO.OrderItem;
using Trendimaa.DTO.Product;
using Trendimaa.DTO.SearchDTOs;
using Trendimaa.DTO.SearchRelated;
using Trendimaa.DTO.Seller;

namespace Trendimaa.BLL.Mapping
{
    public class Profiles : Profile
    {
        public Profiles()
        {
            CreateMap<AppUser, AppUserDTO>().ReverseMap();
            CreateMap<Image, ImageDTO>().ReverseMap();
            CreateMap<Product, ProductSellerListDto>().ReverseMap();
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Specification, SpecificationDTO>().ReverseMap();
            CreateMap<Variety, VarietyDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<SubCategory, CategoryDTO>().ReverseMap();
            CreateMap<SubSubCategory, CategoryDTO>().ReverseMap();
            CreateMap<SubSubCategory, CategoryHomeDTO>().ReverseMap();
            CreateMap<SubCategory, CategoryHomeDTO>().ReverseMap();
            CreateMap<Category, CategoryHomeDTO>().ReverseMap();




            CreateMap<Seller, SellerDTO>().ReverseMap();
            CreateMap<Comment, CommentDTO>().ReverseMap();
            CreateMap<Campaign, CampaignDTO>().ReverseMap();
            CreateMap<Campaign, MainHomeCampaignDTO>().ReverseMap();
            CreateMap<Card, CardDTO>().ReverseMap();
            CreateMap<CardItem, CardItemDTO>().ReverseMap();
            CreateMap<Address, AddressDTO>().ReverseMap();
            CreateMap<SearchRelatedService, SearchRelatedDTO>().ReverseMap();
            CreateMap<HomeProductCardDto, Product>().ReverseMap();
            CreateMap<SubCategoryDTO, SubCategory>().ReverseMap();
            CreateMap<SubSubCategoryDto, SubSubCategory>().ReverseMap();

            CreateMap<SearchCategoryDTO, SubSubCategory>().ReverseMap();
            CreateMap<SearchCategoryDTO, SubCategory>().ReverseMap();
            CreateMap<SearchCategoryDTO, Category>().ReverseMap();


            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<OrderItem, OrderItemDTO>().ReverseMap();
            CreateMap<CompareProductDTO, Product>().ReverseMap();
            CreateMap<CompareSpecificationDTO, Specification>().ReverseMap();
            CreateMap<Category, CategoryHomeDTO>().ReverseMap();
            CreateMap<CommentDTO, Comment>().ReverseMap();

            CreateMap<LatestSearchWord, SearchRelated>().ReverseMap();

            CreateMap<BasicProductCardDTO, Product>().ReverseMap();
            CreateMap<LatestSearchWord, Product>().ReverseMap();

            CreateMap<SearchProductDTO, Product>().ReverseMap();
            CreateMap<Seller, SellerCardDTO>().ReverseMap();
            CreateMap<Product, ProductDetailDTO>().ReverseMap();
            
            CreateMap<Notification, NotificationDTO>().ReverseMap();
            
            CreateMap<Answer, AnswerDTO>().ReverseMap();
            CreateMap<Question, QuestionDTO>().ReverseMap();

        }
    }
}
