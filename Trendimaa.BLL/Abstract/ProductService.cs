using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Data;
using System.Device.Location;
using System.Xml;
using Trendeimaa.Entities;
using Trendimaa.BLL.Helper;
using Trendimaa.BLL.Interface;
using Trendimaa.Common;
using Trendimaa.DAL.Context;
using Trendimaa.DAL.UnitOfWork;
using Trendimaa.DTO;
using Trendimaa.DTO.Product;
using Trendimaa.DTO.SearchDTOs;

namespace Trendimaa.BLL.Abstract
{
    public class ProductService : Service<Product>, IProductService
    {
        public readonly IMapper _mapper;
        public readonly TrendimaaContext _context;
        public readonly IValidator<Product> _validator;
        public readonly IUOW _uow;
        public ProductService(IValidator<Product> validator, IUOW uow, TrendimaaContext context, IMapper mapper) : base(validator, uow, context)
        {
            _validator = validator;
            _uow = uow;
            _context = context;
            _mapper = mapper;
        }

        public async Task<IResponse<List<CompareProductDTO>>> CompareProducts(List<int> productIds)
        {
            var mappedProducts = new List<CompareProductDTO>();
            foreach (var productId in productIds)
            {
                var product = await _context.Products.Where(i => i.Id == productId)
                    .Include(i => i.Seller)
                    .Include(i => i.Specifications)
                    .Include(i => i.Varieties)
                    .FirstOrDefaultAsync();
                var mappedProduct = _mapper.Map<CompareProductDTO>(product);
                mappedProduct.SellerName = product.Seller.CompanyName;
                mappedProduct.SellerImageUrl = product.Seller.Image.Path;
                mappedProducts.Add(mappedProduct);
            }
            foreach (var product in mappedProducts)
            {
                var productVariants = product.Specifications.Select(i => i.Title).ToList();
                var allProductVariants = mappedProducts.SelectMany(i => i.Specifications.Select(i => i.Title)).ToList();
                var sameVariants = allProductVariants.Intersect(productVariants).ToList();
                productVariants.RemoveAll(i => !sameVariants.Contains(i));
                if (productVariants.Count > 0)
                {
                    mappedProducts.Remove(product);
                }


            }
            return new Response<List<CompareProductDTO>>(ResponseType.Success, mappedProducts);


        }
        public async Task<IResponse<List<HomeProductCardDto>>> GetAroundMostLooked(double latitude, double longitude)
        {
            var sellerLocation = new GeoCoordinate(latitude, longitude);

            double DegreeInKm = 111.32;
            double halfSide = 20 / 2;
            double latHalfSide = halfSide / DegreeInKm;
            double lonHalfSide = halfSide / (DegreeInKm * Math.Cos(Math.PI * latitude / 180.0));

            double topLat = latitude + latHalfSide;
            double bottomLat = latitude - latHalfSide;
            double leftLon = longitude - lonHalfSide;
            double rightLon = longitude + lonHalfSide;

            var relatedProducts = await _context.SearchRelateds
                .Where(i => i.Latitude <= topLat && i.Longitude >= bottomLat)
                .Where(i => i.Longitude >= leftLon && i.Longitude <= rightLon)
                .Include(i => i.Product)
                .GroupBy(sr => sr.ProductId) // ProductId'ye göre grupluyoruz
                .OrderByDescending(group => group.Count()) // Grupların sayısına göre azalan şekilde sıralıyoruz (yani en fazla olanlar önce gelecek)
                .Select(group => group.First().Product) // Her gruptan sadece bir öğeyi seçiyoruz (ProductId'ye göre grupladığımız için aynı ProductId'ye sahip olanlar arasından birini seçmek yeterli)
                .Distinct()
                .OrderByDescending(i => i.NumberOfClicks)
                .ToListAsync();
            var mapped = _mapper.Map<List<HomeProductCardDto>>(relatedProducts);
            return new Response<List<HomeProductCardDto>>(ResponseType.Success, mapped);


        }
        public async Task<IResponse<List<HomeProductCardDto>>> GetHomeRelatedProducts(int userId)
        {
            var realtedProducts = await _context.SearchRelateds
                 .Where(i => i.AppUserId == userId)
                 .GroupBy(i => i.CategoryId)
                 .OrderByDescending(group => group.Count())
                 .SelectMany(i => i.First().Category.Products.OrderByDescending(i => i.NumberOfClicks).Take(20))
                .Include(i => i.Images)
                 .ToListAsync();
            if (realtedProducts.Count > 0)
            {
                realtedProducts = await _context.Products
                    .OrderByDescending(i => i.NumberOfClicks)
                    .Take(20)
                    .ToListAsync();
            }
            var mapped = _mapper.Map<List<HomeProductCardDto>>(realtedProducts);
            return new Response<List<HomeProductCardDto>>(ResponseType.Success, mapped);




        }
        public async Task<IResponse<List<HomeProductCardDto>>> GetLastLookedProducts(int userId)
        {
            var realtedProducts = await _context.SearchRelateds
               .Where(i => i.AppUserId == userId)
               .GroupBy(i => i.Product)
               .OrderByDescending(group => group.Count())
               .Select(i => i.First().Product)
               .Take(20)
               .Include(i => i.Images)
               .ToListAsync();

            if (realtedProducts.Count > 0)
            {
                realtedProducts = await _context.Products
                    .OrderByDescending(i => i.NumberOfClicks)
                    .Skip(20)
                    .Take(20)
                    .ToListAsync();
            }
            var mapped = _mapper.Map<List<HomeProductCardDto>>(realtedProducts);
            return new Response<List<HomeProductCardDto>>(ResponseType.Success, mapped);
        }
        public async Task<IResponse<List<BasicProductCardDTO>>> GetLatestSearchProducts(int userId)
        {
            var lastLookedProduct = await _context.SearchRelateds
                .Where(i => i.AppUserId == userId)
                .Select(i => i.Product)
                .Take(5)
                .ToListAsync();

            var mapped = _mapper.Map<List<BasicProductCardDTO>>(lastLookedProduct);
            return new Response<List<BasicProductCardDTO>>(ResponseType.Success, mapped);


        }
        public async Task<IResponse<List<LatestSearchWord>>> GetLastestSearchedWords(int userId)
        {
            var searchRelateds = await _context.SearchRelateds
                .Where(i => i.AppUserId == userId)
                .Take(10)
                .ToListAsync();
            var mapped = _mapper.Map<List<LatestSearchWord>>(searchRelateds);
            return new Response<List<LatestSearchWord>>(ResponseType.Success, mapped);
        }

        public async Task<IResponse<List<SearchCategoryDTO>>> GetSearchCategoryList(string word)
        {
            word = HelperFunctions.OneCharacterReplace(word);
            var cats = await _context.Categories
                  .Where(i => HelperFunctions.OneCharacterReplace(i.Name).Contains(HelperFunctions.OneCharacterReplace(word)) || HelperFunctions.CharacterReplace(i.Products.Select(z => z.Name).ToList()).Contains(word))
                  .ToListAsync();
            var mapped = _mapper.Map<List<SearchCategoryDTO>>(cats);
            return new Response<List<SearchCategoryDTO>>(ResponseType.Success, mapped);
        }

        public async Task<IResponse<List<SearchSubCategoryDTO>>> GetSearchSubCategoryList(string word)
        {
            word = HelperFunctions.OneCharacterReplace(word);
            var subcats = await _context.SubCategoies
                .Where(i => HelperFunctions.OneCharacterReplace(i.Name).Contains(HelperFunctions.OneCharacterReplace(word)) || HelperFunctions.CharacterReplace(i.Products.Select(z => z.Name).ToList()).Contains(word))
                .Include(i => i.Products)
                    .ThenInclude(p => p.Category)
                .ToListAsync();
            var mapped = _mapper.Map<List<SearchSubCategoryDTO>>(subcats);
            return new Response<List<SearchSubCategoryDTO>>(ResponseType.Success, mapped);
        }

        public async Task<IResponse<List<SearchSubSubCategoryDTO>>> GetSearchSubSubCategoryList(string word)
        {
            word = HelperFunctions.OneCharacterReplace(word);
            var subsubcats = await _context.SubSubCategories
                  .Where(i => HelperFunctions.OneCharacterReplace(i.Name).Contains(HelperFunctions.OneCharacterReplace(word)) || HelperFunctions.CharacterReplace(i.Products.Select(z => z.Name).ToList()).Contains(word))
                  .Include(i => i.SubCategory)
                  .ThenInclude(i => i.Category)
                  .ToListAsync();
            var mapped = _mapper.Map<List<SearchSubSubCategoryDTO>>(subsubcats);
            return new Response<List<SearchSubSubCategoryDTO>>(ResponseType.Success, mapped);
        }

        public async Task<IResponse<List<SearchProductDTO>>> GetSearchProductList(string word)
        {
            word = HelperFunctions.OneCharacterReplace(word);
            var products = await _context.Products
                .Where(i => HelperFunctions.OneCharacterReplace(i.Name).Contains(HelperFunctions.OneCharacterReplace(word)) || HelperFunctions.OneCharacterReplace(i.Description).Contains(HelperFunctions.OneCharacterReplace(i.Description)))
                .Include(i => i.Category)
                .Include(i => i.SubCategory)
                .Include(i => i.SubSubCategory)
                    .ThenInclude(i => i.SubCategory)
                    .ThenInclude(i => i.Category)
                .ToListAsync();
            var mapped = _mapper.Map<List<SearchProductDTO>>(products);
            return new Response<List<SearchProductDTO>>(ResponseType.Success, mapped);
        }

        public async Task<IResponse<List<SearchProductDTO>>> GetSearchProductResult(string word, int? catId, int? subCatId, int? subsubCatId)
        {
            word += HelperFunctions.OneCharacterReplace(word);
            var products = new List<Product>();
            if (catId != null)
            {
                products = await _context.Categories
                   .Where(i => i.Id == catId)
                   .SelectMany(i => i.Products.Where(p => HelperFunctions.OneCharacterReplace(p.Name).Contains(word) || HelperFunctions.OneCharacterReplace(p.Description).Contains(word)))
                   .Include(p => p.Images)
                   .Include(p => p.Seller)
                   .OrderBy(i => i.NumberOfClicks)
                   .ToListAsync();
            }
            else if (subCatId != null)
            {
                products = await _context.SubCategoies
                 .Where(i => i.Id == subCatId)
                 .SelectMany(i => i.Products.Where(p => HelperFunctions.OneCharacterReplace(p.Name).Contains(word) || HelperFunctions.OneCharacterReplace(p.Description).Contains(word)))
                 .OrderBy(i => i.NumberOfClicks)
                 .Include(p => p.Images)
                 .Include(p => p.Seller)
                 .ToListAsync();
            }
            else if (subsubCatId != null)
            {
                products = await _context.SubSubCategories
                 .Where(i => i.Id == subsubCatId)
                 .SelectMany(i => i.Products.Where(p => HelperFunctions.OneCharacterReplace(p.Name).Contains(word) || HelperFunctions.OneCharacterReplace(p.Description).Contains(word)))
                 .OrderBy(i => i.NumberOfClicks)
                 .Include(p => p.Images)
                 .Include(p => p.Seller)
                 .ToListAsync();
            }
            products = await _context.Products
                 .Where(p => HelperFunctions.OneCharacterReplace(p.Name).Contains(word) || HelperFunctions.OneCharacterReplace(p.Description).Contains(word))
                 .OrderBy(i => i.NumberOfClicks)
                 .Include(p => p.Images)
                 .Include(p => p.Seller)
                 .ToListAsync();
            var mapped = _mapper.Map<List<SearchProductDTO>>(products);
            return new Response<List<SearchProductDTO>>(ResponseType.Success, mapped);
        }

        public async Task<IResponse<List<BasicProductCardDTO>>> ChangeSearchProductResult(ChangeSearchListDTO dto)
        {
            var products = new List<Product>();
            products = await _context.Products
            .Include(i => i.Specifications)
            .Include(i => i.Varieties)
            .ToListAsync();
            if (dto.Brands != null)
                products.Where(i => dto.Brands.Contains(i.Brand)).ToList();
            if (dto.MaxPrice != null)
                products.Where(i => i.StockPrice < dto.MaxPrice).ToList();
            if (dto.MinPrice != null)
                products.Where(i => i.StockPrice < dto.MinPrice).ToList();
            if (dto.CategoryIds != null)
                products.Where(i => dto.CategoryIds.Contains(i.CategoryId.Value)).ToList();
            if (dto.SubCategoryIds != null)
                products.Where(i => dto.SubCategoryIds.Contains(i.SubCategoryId.Value)).ToList();
            if (dto.SubSubCategoryIds != null)
                products.Where(i => dto.SubSubCategoryIds.Contains(i.SubSubCategoryId.Value)).ToList();
            if (dto.Specifications != null)
                products = products.Where(i => i.Specifications.Any(spec => dto.Specifications.Contains(spec))).ToList();
            if (dto.Varieties != null)
                products = products.Where(i => i.Varieties.Any(var => dto.Varieties.Contains(var))).ToList();
            var mapped=_mapper.Map<List<BasicProductCardDTO>>(products);
            return new Response<List<BasicProductCardDTO>>(ResponseType.Success,mapped);
        }
    }
}
