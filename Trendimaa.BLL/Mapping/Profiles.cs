using AutoMapper;
using Trendeimaa.Entities;
using Trendeimaa.Entities.CategoryFolder;
using Trendimaa.BLL.Abstract;
using Trendimaa.DTO;
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
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Specification, SpecificationDTO>().ReverseMap();
            CreateMap<Variety, VarietyDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Seller, SellerDTO>().ReverseMap();
            CreateMap<Comment, CommentDTO>().ReverseMap();
            CreateMap<QuestionAnswer, QuestionAnswerDTO>().ReverseMap();
            CreateMap<Campaign, CampaignDTO>().ReverseMap();
            CreateMap<Campaign, MainHomeCampaignDTO>().ReverseMap();
            CreateMap<Card, CardDTO>().ReverseMap();
            CreateMap<CardItem, CardItemDTO>().ReverseMap();
            CreateMap<Address, AddressDTO>().ReverseMap();
            CreateMap<SearchRelatedService, SearchRelatedDTO>().ReverseMap();
            CreateMap<HomeProductCardDto, Product>().ReverseMap();
            CreateMap<SubCategoryDTO, SubCategory>().ReverseMap();
            CreateMap<SubSubCategoryDto, SubSubCategory>().ReverseMap();
            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<OrderItem, OrderItemDTO>().ReverseMap();
            CreateMap<CompareProductDTO, Product>().ReverseMap();
            CreateMap<CompareSpecificationDTO, Specification>().ReverseMap();

            CreateMap<BasicProductCardDTO, Product>().ReverseMap();
            CreateMap<LatestSearchWord, Product>().ReverseMap();

            CreateMap<SearchProductDTO, Product>().ReverseMap();
            CreateMap<Seller, SellerCardDTO>().ReverseMap();
            
            CreateMap<Notification, NotificationDTO>().ReverseMap();

        }
    }
}
