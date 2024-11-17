using Trendeimaa.Entities;
using Trendeimaa.Entities.Interface;
using Trendimaa.Common.Enum;
using Trendimaa.DTO.CategoryDTOs;
using Trendimaa.DTO.Seller;

namespace Trendimaa.DTO.Product
{
    public class ProductDetailDTO:BaseEntity    
    {
        public string Name { get; set; }
        public string Brand { get; set; }
        public int StockPiece { get; set; } 
        public string Detail { get; set; }
        public double StockPrice { get; set; }
        public int? Discount { get; set; } = 0;
        public int? CashbackAmount { get; set; } = 0;
        public double? FreeShippingLimitPrice { get; set; } = 0;
        public double ShippingPrice { get; set; }
        public string Description { get; set; }
        public double Rate { get; set; }

        public Language Language { get; set; }
        public Currency Currency { get; set; }

        public int? CategoryId { get; set; }
        public CategoryHomeDTO? Category { get; set; }
        public int? SubCategoryId { get; set; }
        public CategoryHomeDTO? SubCategory { get; set; }
        public int? SubSubCategoryId { get; set; }
        public CategoryHomeDTO? SubSubCategory { get; set; }
        public int? SellerId { get; set; }
        public SellerCardDTO? Seller { get; set; }

        public List<Specification> Specifications { get; set; }
        public List<ImageDTO> Images { get; set; }
        public List<Variety> Varieties { get; set; }
        public List<Comment> Comments { get; set; }
        public List<QuestionDTO> Questions{ get; set; }
        public List<VarietiesPriceDto>? VarietiesPrices{ get; set; }
        
    
    }
}
