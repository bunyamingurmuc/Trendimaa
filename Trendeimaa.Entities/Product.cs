using Trendeimaa.Entities.CategoryFolder;
using Trendeimaa.Entities.Interface;
using Trendimaa.Common.Enum;

namespace Trendeimaa.Entities
{
    public class Product : BaseEntity
    {
        public Product()
        {
            Specifications = new List<Specification>();
            Images = new List<Image>();
            Varieties = new List<Variety>();
            Comments = new List<Comment>();
            QuestionAnswers = new List<QuestionAnswer>();
            CardItems = new List<CardItem>();
            SearchRelateds = new List<SearchRelated>();
            OrderItems = new List<OrderItem>();


        }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Detail { get; set; }
        public int StockPrice { get; set; }
        public double? FreeShippingLimitPrice { get; set; } = 0;
        public double ShippingPrice { get; set; }
        public string Description { get; set; }
        public double Rate { get; set; }
        public bool? IsHomeProduct { get; set; } = false;
        public bool? IsStarProduct { get; set; }= false;
        public int? NumberOfClicks { get; set; }

        public Language Language { get; set; }
        public Currency Currency { get; set; }

        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
        public int? SubCategoryId { get; set; }
        public SubCategory? SubCategory { get; set; }
        public int? SubSubCategoryId { get; set; }
        public SubSubCategory? SubSubCategory { get; set; }
        public int? SellerId { get; set; }
        public Seller? Seller { get; set; }



        public List<Specification> Specifications { get; set; }
        public List<Image> Images { get; set; }
        public List<Variety> Varieties { get; set; }
        public List<Comment> Comments { get; set; }
        public List<QuestionAnswer> QuestionAnswers { get; set; }
        public List<CardItem> CardItems { get; set; }
        public List<SearchRelated> SearchRelateds { get; set; }
        public List<OrderItem> OrderItems{ get; set; }

    }
}
