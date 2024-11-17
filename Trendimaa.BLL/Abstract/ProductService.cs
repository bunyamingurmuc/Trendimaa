using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Data;
using System.Device.Location;
using System.IO.Compression;
using Trendeimaa.Entities;
using Trendimaa.BLL.Helper;
using Trendimaa.BLL.Interface;
using Trendimaa.Common;
using Trendimaa.Common.Enum;
using Trendimaa.DAL.Context;
using Trendimaa.DAL.UnitOfWork;
using Trendimaa.DTO;
using Trendimaa.DTO.Listing;
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
           .Where(i => i.ProductId != null && i.AppUserId == userId)
           .Include(i => i.Product.Images)
           .Select(i => i.Product)
           .Take(5)
           .ToListAsync();

            var mapped = _mapper.Map<List<BasicProductCardDTO>>(lastLookedProduct);
            return new Response<List<BasicProductCardDTO>>(ResponseType.Success, mapped);


        }
        public async Task<IResponse<List<BasicProductCardDTO>>> GetMainHomeProducts()
        {
            var products = await _context.Products
                .Where(i => i.IsHomeProduct == true)
                .Include(i => i.Images)
                .Take(20)
                .ToListAsync();

            var mapped = _mapper.Map<List<BasicProductCardDTO>>(products);
            return new Response<List<BasicProductCardDTO>>(ResponseType.Success, mapped);


        }
        public async Task<IResponse<List<LatestSearchWord>>> GetLastestSearchedWords(int userId)
        {
            var searchRelateds = await _context.SearchRelateds
                .Where(i => i.AppUserId == userId)
                .Where(i => i.ProductId == null)
                .Where(i => i.SubCategoryId == null)
                .Where(i => i.SubSubCategoryId == null)
                .Take(10)
                .ToListAsync();
            var mapped = _mapper.Map<List<LatestSearchWord>>(searchRelateds);
            return new Response<List<LatestSearchWord>>(ResponseType.Success, mapped);
        }

        public async Task<IResponse<List<SearchCategoryDTO>>> GetSearchCategoryList(string word)
        {
            word = HelperFunctions.OneCharacterReplace(word);
            var cats = await _context.Categories
                .Where(p =>
                p.Name.Replace(",", "")
                    .Replace(" ", "")
                    .Replace("ş", "s")
                    .Replace("ö", "o")
                    .Replace("ğ", "g")
                    .Replace("ı", "i")
                    .Replace("I", "i")
                    .Replace("(", "")
                    .Replace(")", "")
                    .Replace("ç", "c")
                    .Replace("ü", "u")
                    .Trim()
                    .ToLower()
                    .Contains(word)).ToListAsync();
            var mapped = _mapper.Map<List<SearchCategoryDTO>>(cats);
            return new Response<List<SearchCategoryDTO>>(ResponseType.Success, mapped);
        }

        public async Task<IResponse<List<SearchCategoryDTO>>> GetSearchSubCategoryList(string word)
        {
            word = HelperFunctions.OneCharacterReplace(word);
            var subcats = await _context.SubCategoies
                .Where(p =>
                p.Name.Replace(",", "")
                    .Replace(" ", "")
                    .Replace("ş", "s")
                    .Replace("ö", "o")
                    .Replace("ğ", "g")
                    .Replace("ı", "i")
                    .Replace("I", "i")
                    .Replace("(", "")
                    .Replace(")", "")
                    .Replace("ç", "c")
                    .Replace("ü", "u")
                    .Trim()
                    .ToLower()
                    .Contains(word)).Include(i => i.Products)
                    .ThenInclude(p => p.Category)
                .ToListAsync();
            var mapped = _mapper.Map<List<SearchCategoryDTO>>(subcats);
            return new Response<List<SearchCategoryDTO>>(ResponseType.Success, mapped);
        }

        public async Task<IResponse<List<SearchCategoryDTO>>> GetSearchSubSubCategoryList(string word)
        {
            word = HelperFunctions.OneCharacterReplace(word);
            var subsubcats = await _context.SubSubCategories
                    .Where(p =>
                p.Name.Replace(",", "")
                    .Replace(" ", "")
                    .Replace("ş", "s")
                    .Replace("ö", "o")
                    .Replace("ğ", "g")
                    .Replace("ı", "i")
                    .Replace("I", "i")
                    .Replace("(", "")
                    .Replace(")", "")
                    .Replace("ç", "c")
                    .Replace("ü", "u")
                    .Trim()
                    .ToLower()
                    .Contains(word))
                  .Include(i => i.SubCategory)
                  .ThenInclude(i => i.Category)
                  .ToListAsync();
            var mapped = _mapper.Map<List<SearchCategoryDTO>>(subsubcats);
            return new Response<List<SearchCategoryDTO>>(ResponseType.Success, mapped);
        }

        public async Task<IResponse<List<SearchProductDTO>>> GetSearchProductList(string word)
        {
            word = HelperFunctions.OneCharacterReplace(word);
            var products = await _context.Products
                 .Where(p =>
                p.Name.Replace(",", "")
                    .Replace(" ", "")
                    .Replace("ş", "s")
                    .Replace("ö", "o")
                    .Replace("ğ", "g")
                    .Replace("ı", "i")
                    .Replace("I", "i")
                    .Replace("(", "")
                    .Replace(")", "")
                    .Replace("ç", "c")
                    .Replace("ü", "u")
                    .Trim()
                    .ToLower()
                    .Contains(word) ||
                p.Description.Replace(",", "")
                    .Replace(" ", "")
                    .Replace("ş", "s")
                    .Replace("ö", "o")
                    .Replace("ğ", "g")
                    .Replace("ı", "i")
                    .Replace("I", "i")
                    .Replace("(", "")
                    .Replace(")", "")
                    .Replace("ç", "c")
                    .Replace("ü", "u")
                    .Trim()
                    .ToLower()
                    .Contains(word))
            .OrderBy(i => i.NumberOfClicks)
            .Include(p => p.Images)
            .Include(p => p.Seller)

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
            word = HelperFunctions.OneCharacterReplace(word);
            var products = new List<Product>();
            products = await _context.Products
            .Where(p =>
                p.Name.Replace(",", "")
                    .Replace(" ", "")
                    .Replace("ş", "s")
                    .Replace("ö", "o")
                    .Replace("ğ", "g")
                    .Replace("ı", "i")
                    .Replace("I", "i")
                    .Replace("(", "")
                    .Replace(")", "")
                    .Replace("ç", "c")
                    .Replace("ü", "u")
                    .Trim()
                    .ToLower()
                    .Contains(word))
            .OrderBy(i => i.NumberOfClicks)
            .Include(p => p.Images)
            .Include(p => p.Seller)
            .ToListAsync();
            var product2 = await _context.Products
                .Where(p => p.Description.Replace(",", "")
                    .Replace(" ", "")
                    .Replace("ş", "s")
                    .Replace("ö", "o")
                    .Replace("ğ", "g")
                    .Replace("ı", "i")
                    .Replace("I", "i")
                    .Replace("(", "")
                    .Replace(")", "")
                    .Replace("ç", "c")
                    .Replace("ü", "u")
                    .Trim()
                    .ToLower()
                    .Contains(word))
                      .OrderBy(i => i.NumberOfClicks)
                      .Include(p => p.Images)
                      .Include(p => p.Seller)
                      .ToListAsync();
            products.AddRange(product2);

            if (catId != null)
            {
                products = products.Where(i => i.CategoryId == catId).ToList();
            }
            else if (subCatId != null)
            {
                products = products.Where(i => i.SubCategoryId == subCatId).ToList();

            }
            else if (subsubCatId != null)
            {
                products = products.Where(i => i.SubSubCategoryId == subsubCatId).ToList();

            }
            products = HelperFunctions.GetUniqueProductsByGroupCode(products);
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
            var mapped = _mapper.Map<List<BasicProductCardDTO>>(products);
            return new Response<List<BasicProductCardDTO>>(ResponseType.Success, mapped);
        }

        public async Task<byte[]> GenerateExcel(int? subcategoryId, int? subSubcategoryId, Language language)
        {
            using (ExcelPackage package = new ExcelPackage())
            {
                var specs = new List<Specification>();
                var varieties = new List<Variety>();
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Product_Template");

                var requiredCellStyle = package.Workbook.Styles.CreateNamedStyle("1");
                requiredCellStyle.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                requiredCellStyle.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Red);
                requiredCellStyle.Style.Border.BorderAround(ExcelBorderStyle.Medium);
                requiredCellStyle.Style.Font.Bold = true;

                var blueStyle = package.Workbook.Styles.CreateNamedStyle("2");
                blueStyle.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                blueStyle.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(197, 224, 180));
                blueStyle.Style.Font.Bold = true;


                int BasicProductInfCount = 12;
                //int VariantCount = varieties.Count();
                //int SpecCount = specs.Count();

                worksheet.Cells[1, 1].Value = "Basic Product Information";
                worksheet.Cells[1, 1].Value = "Basic Product Information";
                for (int i = 1; i <= BasicProductInfCount; i++)
                {
                    var cell = worksheet.Cells[1, i];
                    cell.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    cell.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(197, 224, 180));
                    cell.Style.Font.Bold = true; // Yazıyı kalın yap
                                                 // Kenarlık ekleme

                }

                // Product Specifications başlıklarını ve hücrelerini ayarlama
                worksheet.Cells[1, BasicProductInfCount + 1].Value = "Product Specifications";
                for (int i = BasicProductInfCount + 1; i <= BasicProductInfCount + specs.Count(); i++)
                {
                    var cell = worksheet.Cells[1, i];
                    cell.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    cell.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(189, 215, 238));
                    cell.Style.Font.Bold = true; // Yazıyı kalın yap
                                                 // Kenarlık ekleme

                }

                // Product Varieties başlıklarını ve hücrelerini ayarlama
                worksheet.Cells[1, BasicProductInfCount + specs.Count() + 1].Value = "Product Varieties";
                for (int i = BasicProductInfCount + specs.Count() + 1; i <= BasicProductInfCount + specs.Count() + varieties.Count(); i++)
                {
                    var cell = worksheet.Cells[1, i];
                    cell.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    cell.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(255, 217, 102));
                    cell.Style.Font.Bold = true; // Yazıyı kalın yap
                                                 // Kenarlık ekleme
                    var borderStyle = ExcelBorderStyle.Medium;

                }



                // Specifications için Zorunlu alanları işaretleme ve orta kalınlıkta kenarlık ekleme
                for (int i = 1; i <= specs.Count(); i++)
                {
                    var cell = worksheet.Cells[2, BasicProductInfCount + i];
                    if (specs[i - 1].IsRequired == true)
                    {
                        cell.Value = "Zorunlu";
                        cell.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        cell.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Red);
                        var borderStyle = ExcelBorderStyle.Medium;
                        cell.Style.Border.BorderAround(borderStyle);
                    }
                }

                // Varieties için Zorunlu alanları işaretleme ve orta kalınlıkta kenarlık ekleme
                for (int i = 1; i <= varieties.Count(); i++)
                {
                    var cell = worksheet.Cells[2, BasicProductInfCount + specs.Count() + i];
                    if (varieties[i - 1].IsRequired == true)
                    {

                        cell.Value = "Zorunlu";
                        cell.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        cell.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Red);

                        var borderStyle = ExcelBorderStyle.Medium;
                        cell.Style.Border.BorderAround(borderStyle);
                    }
                }





                // Specifications ve Varieties için Zorunlu alanları işaretleme
                for (int i = 1; i <= specs.Count(); i++)
                {
                    var cell = worksheet.Cells[2, BasicProductInfCount + i];
                    if (specs[i - 1].IsRequired == true)
                    {
                        cell.Value = "Zorunlu";
                        cell.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        cell.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Red);
                    }
                    else
                    {
                        cell.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        cell.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(189, 215, 238));
                    }
                }

                for (int i = 1; i <= varieties.Count(); i++)
                {
                    var cell = worksheet.Cells[2, BasicProductInfCount + specs.Count() + i];
                    if (varieties[i - 1].IsRequired == true)
                    {
                        cell.Value = "Zorunlu";
                        cell.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        cell.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Red);
                    }
                    else
                    {
                        cell.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        cell.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(255, 217, 102));
                    }
                }



                // Product Specifications başlıklarını boyama
                for (int i = BasicProductInfCount + 1; i <= BasicProductInfCount + specs.Count(); i++)
                {
                    worksheet.Cells[3, i].Value = specs[i - BasicProductInfCount - 1].Title;

                }

                // Varyant başlıklarını boyama
                for (int i = BasicProductInfCount + specs.Count() + 1; i <= BasicProductInfCount + specs.Count() + varieties.Count(); i++)
                {
                    worksheet.Cells[3, i].Value = specs[i - (BasicProductInfCount + specs.Count() + 1)].Title;
                }


                worksheet.Cells[2, 1].Value = "Zorunlu";
                worksheet.Cells[2, 1].StyleName = "1";
                worksheet.Cells[2, 1].Style.Border.BorderAround(ExcelBorderStyle.Medium);
                worksheet.Cells[2, 2].Value = "Zorunlu";
                worksheet.Cells[2, 2].StyleName = "1";
                worksheet.Cells[2, 2].Style.Border.BorderAround(ExcelBorderStyle.Medium);
                worksheet.Cells[2, 3].Value = "Zorunlu";
                worksheet.Cells[2, 3].StyleName = "1";
                worksheet.Cells[2, 3].Style.Border.BorderAround(ExcelBorderStyle.Medium);
                worksheet.Cells[2, 4].Value = "Zorunlu";
                worksheet.Cells[2, 4].StyleName = "1";
                worksheet.Cells[2, 4].Style.Border.BorderAround(ExcelBorderStyle.Medium);
                worksheet.Cells[2, 5].Value = "Zorunlu";
                worksheet.Cells[2, 5].StyleName = "1";
                worksheet.Cells[2, 5].Style.Border.BorderAround(ExcelBorderStyle.Medium);

                worksheet.Cells[2, 6].StyleName = "2";
                worksheet.Cells[2, 7].StyleName = "2";
                worksheet.Cells[2, 8].StyleName = "2";
                worksheet.Cells[2, 9].StyleName = "2";
                worksheet.Cells[2, 10].StyleName = "2";

                worksheet.Cells[2, 11].Value = "Zorunlu";
                worksheet.Cells[2, 11].StyleName = "1";
                worksheet.Cells[2, 11].Style.Border.BorderAround(ExcelBorderStyle.Medium);
                worksheet.Cells[2, 12].Value = "Zorunlu";
                worksheet.Cells[2, 12].StyleName = "1";
                worksheet.Cells[2, 12].Style.Border.BorderAround(ExcelBorderStyle.Medium);



                worksheet.Cells[3, 1].Value = "Name";
                worksheet.Cells[3, 2].Value = "Brand";
                worksheet.Cells[3, 3].Value = "Detail";
                worksheet.Cells[3, 4].Value = "StockPrice";
                worksheet.Cells[3, 5].Value = "Description";
                worksheet.Cells[3, 6].Value = "Image 1";
                worksheet.Cells[3, 7].Value = "Image 2";
                worksheet.Cells[3, 8].Value = "Image 3";
                worksheet.Cells[3, 9].Value = "Image 4";
                worksheet.Cells[3, 10].Value = "Image 5";
                worksheet.Cells[3, 11].Value = "Language";
                worksheet.Cells[3, 12].Value = "Currency";

                string filePath = @"C:\Users\zeynel\Desktop\Product_Template.xlsx";
                FileInfo file = new FileInfo(filePath);
                package.SaveAs(file);
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    await package.SaveAsAsync(memoryStream);
                    return memoryStream.ToArray();
                }
            }

        }

        public Task<List<Product>> ImportExcel(int? subcategoryId, int? subSubcategoryId, Language language, byte[] excelData)
        {
            throw new NotImplementedException();
        }

        public async Task<IResponse<CSellerProductsDto>> GetSellerProductListsWithCount(int sellerId, int page, int quantity, string? word, int? catId, int? subCatId, int? subSubCatId)
        {
            var data = await _context.Products
               .Where(i => i.SellerId == sellerId)
               .Include(i => i.Images)
               .OrderByDescending(i => i.CreatedDate)
               .AsNoTracking()
               .ToListAsync();

            var Count = 0;
            if (word != null && word.Length >= 3)
            {
                word = word.ToLower().Trim();
                data = data
               .Where(i => i.Name.ToLower().Contains(word) || i.Description.ToLower().Contains(word))
               .OrderByDescending(i => i.CreatedDate)
               .ToList();
            }
            if (catId != null)
                data = data.Where(i => i.CategoryId == catId).ToList();
            if (subCatId != null)
                data = data.Where(i => i.SubCategoryId == subCatId).ToList();
            if (subCatId != null)
                data = data.Where(i => i.SubSubCategoryId == subSubCatId).ToList();
            Count = data.Count();
            var mapped = _mapper.Map<List<ProductSellerListDto>>(data);
            CSellerProductsDto dto = new CSellerProductsDto()
            {
                TotalProductCount = Count,
                Products = mapped.Skip((page - 1) * quantity).Take(quantity).ToList(),
            };


            return new Response<CSellerProductsDto>(ResponseType.Success, dto);
        }

        public async Task<IResponse<ProductDetailDTO>> GetProductDetail(int productId)
        {


            var product = await _context.Products.Where(i => i.Id == productId)
                //.Include(i => i.Specifications)
                //.Include(i => i.Varieties)
                .Include(i => i.Images)
                .Include(i => i.Category)
                .Include(i => i.Comments).ThenInclude(i => i.Images)
                .Include(i => i.Seller).ThenInclude(i => i.Image)
                //.Include(i => i.Questions)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            var varietiesPriceDto = new List<VarietiesPriceDto>();
            var varietiesProducts = await _context.Products.Where(i => i.GroupCode == product.GroupCode)
                .Include(i => i.Varieties)
                .ToListAsync();
            foreach (var varietiesProduct in varietiesProducts)
            {
                if (varietiesProduct.Varieties != null)
                {
                    foreach (var variety in varietiesProduct.Varieties)
                    {
                        varietiesPriceDto.Add(new VarietiesPriceDto()
                        {
                            Id = varietiesProduct.Id,
                            IsStock = varietiesProduct.IsStock,
                            VarietyName = variety.VarietyName,
                            StockPrice = varietiesProduct.StockPrice,
                        });
                    }
                }
            }

            var mapped = _mapper.Map<ProductDetailDTO>(product);
            mapped.VarietiesPrices = varietiesPriceDto;
            return new Response<ProductDetailDTO>(ResponseType.Success, mapped);
        }

        public async Task<IResponse<List<BasicProductCardDTO>>> GetSameProducts(int productId)
        {
            var product = await _context.Products.
                  Where(i => i.Id == productId)
                  .FirstOrDefaultAsync();
            var sameProducts = _context.Products.
                Where(i => i.Name.ToLower().Contains(product.Name.ToLower()))
                .Include(i => i.Images)
                .ToListAsync();
            var mapped = _mapper.Map<List<BasicProductCardDTO>>(sameProducts);
            return new Response<List<BasicProductCardDTO>>(ResponseType.Success, mapped);
        }

        public async Task<IResponse<List<BasicProductCardDTO>>> GetCategoriesProduducts(int? cateid, int? subcateid, int? subsubcateid)
        {
            var products = new List<Product>();
            if (cateid != null)
                products = await _context.Products.Include(i => i.Images).Where(i => i.CategoryId == cateid).AsNoTracking().ToListAsync();
            if (subcateid != null)
                products = await _context.Products.Include(i => i.Images).Where(i => i.SubCategoryId == subcateid).AsNoTracking().ToListAsync();
            if (subsubcateid != null)
                products = await _context.Products.Include(i => i.Images).Where(i => i.SubSubCategoryId == subcateid).AsNoTracking().ToListAsync();
            var mapped = _mapper.Map<List<BasicProductCardDTO>>(products);
            return new Response<List<BasicProductCardDTO>>(ResponseType.Success, mapped);

        }

        public async Task<IResponse<List<ProductSellerDTO>>> GetOtherProductSellers(int? productId)
        {
            var product = await _context.Products.Where(i => i.Id == productId).FirstOrDefaultAsync();
            var products = _context.Products.
                Include(i => i.Seller).
                Include(i => i.Images).
                Where(i => i.Name == product.Name).ToList();
            var mapped = _mapper.Map<List<ProductSellerDTO>>(products);
            return new Response<List<ProductSellerDTO>>(ResponseType.Success, mapped);
        }

        public async Task<IResponse<List<BasicProductCardDTO>>> GetSellerStockProducts(int? sellerId)
        {
            var products = await _context.Products.Where(i => i.SellerId == sellerId && i.StockPiece > 0)
                 .Include(i => i.Images)
                 .ToListAsync();
            var mapped = _mapper.Map<List<BasicProductCardDTO>>(products);
            return new Response<List<BasicProductCardDTO>>(ResponseType.Success, mapped);
        }

        public async Task<IResponse<List<BasicProductCardDTO>>> GetSellerNotStockProducts(int? sellerId)
        {
            var products = await _context.Products.Where(i => i.SellerId == sellerId && i.StockPiece == 0)
                .Include(i => i.Images)
                .ToListAsync();
            var mapped = _mapper.Map<List<BasicProductCardDTO>>(products);
            return new Response<List<BasicProductCardDTO>>(ResponseType.Success, mapped);
        }

        public async Task<IResponse<List<BasicProductCardDTO>>> GetSellerNotConfirmedProducts(int? sellerId)
        {
            var products = await _context.Products.Where(i => i.SellerId == sellerId && i.ConfirmStatus == ConfirmStatus.pending)
                .Include(i => i.Images)
                .ToListAsync();
            var mapped = _mapper.Map<List<BasicProductCardDTO>>(products);
            return new Response<List<BasicProductCardDTO>>(ResponseType.Success, mapped);

        }

        public async Task<IResponse<BasicProductCardDTO>> GetProductWithBarcode(string StockCode)
        {
            StockCode = StockCode.Trim();
            var data = await _context.Products.Where(i => i.StockCode == StockCode).FirstOrDefaultAsync();
            var mapped = _mapper.Map<BasicProductCardDTO>(data);
            return new Response<BasicProductCardDTO>(ResponseType.Success, mapped);
        }

        public async Task<IResponse<Product>> UpdateProduct(Product product)
        {
            //var uProduct =await _context.Products.Where(i => i.Id == product.Id)
            //     .Include(i => i.Specifications)
            //     .Include(i => i.Varieties)
            //     .FirstOrDefaultAsync();
            //var specs=product.Specifications;
            //var varieties=product.Varieties;
            throw new NotImplementedException();

        }

        public async Task Free()
        {
            var data = _context.Products.ToList();
            var unchanged = data;
            foreach (var item in data)
            {
                item.StockCode = "TRD121535";
                await UpdateAsync(item);
            }
        }

        public async Task<IResponse> ApplyDiscount(List<int> productIds, int percent, double price)
        {
            var products = await _context.Products.Where(i => productIds.Contains(i.Id.Value)).ToListAsync();
            if (percent != null)
                foreach (var product in products)
                {
                    var updated = await UpdateAsync(product);
                }
            return new Response(ResponseType.Success);

        }

        public async Task<IResponse<List<SearchProductDTO>>> GetSearchProductVarietiesResult(SearchParamtersDTO dto, string word, int count, int page)
        {
            var products = _context.Products.AsQueryable();
            if (word.Length>=2)
            {
                word = HelperFunctions.OneCharacterReplace(word);
                 products = _context.Products.Where(i => i.Brand.Contains(word) && i.Detail.Contains(word))
                                    .AsQueryable();
            }
           
            if (dto.MinPrice != null)
                products.Where(i => i.StockPrice >= dto.MinPrice).AsQueryable();
            if (dto.MaxPrice != null)
                products.Where(i => i.StockPrice <= dto.MaxPrice).AsQueryable();
            if (dto.Brands != null)
                products.Where(i => dto.Brands.Contains(i.Brand)).AsQueryable();
            if (dto.SubSubCategoryId != null)
                products.Where(i => i.SubSubCategoryId == dto.SubSubCategoryId).AsQueryable();
            if (dto.SubCategoryId != null)
                products.Where(i => i.SubCategoryId == dto.SubCategoryId).AsQueryable();
            if (dto.CategoryId != null)
                products.Where(i => i.CategoryId == dto.CategoryId).AsQueryable();
            if (dto.Specifications != null)
            {
                products.Where(product => product.Specifications
                    .Any(spec => dto.Specifications
                        .Any(s => s.Title == spec.Title && s.Description == spec.Description))).AsQueryable();
            }
            if (dto.Varieties != null)
            {
                products.Where(product => product.Varieties
                    .Any(variet => dto.Varieties
                        .Any(s => s.VarietyName == variet.VarietyName && s.Description == variet.Description))).AsQueryable();
            }
            products=  products.Include(i=>i.Images).OrderBy(i=>i.NumberOfClicks);
            await products.ToListAsync();
            var mapped=_mapper.Map<List<SearchProductDTO>>(products);
            return new Response<List<SearchProductDTO>>(ResponseType.Success, mapped);
        }
    }
}

