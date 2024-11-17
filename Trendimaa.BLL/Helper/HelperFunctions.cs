using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trendeimaa.Entities;

namespace Trendimaa.BLL.Helper
{
    public static class HelperFunctions
    {
        public static byte[] GenerateExcelTemplate(int SubCategoryId)
        {
            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Product_Template");

                // Başlık satırlarını ekleme
                worksheet.Cells["A1"].Value = "Temel Ürün Bilgileri";
                worksheet.Cells["B1"].Value = "Varyant Tipi";
                worksheet.Cells["C1"].Value = "Ürün Tipi";

                // Temel ürün bilgileri kolon başlıkları
                worksheet.Cells["A2"].Value = "Ürün Adı";
                worksheet.Cells["B2"].Value = "Renk";
                worksheet.Cells["C2"].Value = "Boyut";

                // Varyant tipi kolon başlıkları
                int variantColumnStartIndex = 3; // Başlangıç sütun indeksi
                List<string> variantColumns = new List<string> { "Varyant Adı", "Zorunlu" }; // Örnek varyant kolonları
                AddColumns(worksheet, variantColumnStartIndex, variantColumns);

                // Ürün tipi kolon başlıkları
                int productColumnStartIndex = variantColumnStartIndex + variantColumns.Count;
                List<string> productColumns = new List<string> { "Özellik Adı", "Zorunlu" }; // Örnek ürün tipi kolonları

                AddColumns(worksheet, productColumnStartIndex, productColumns);

                // Excel dosyasını belleğe yazma
                return package.GetAsByteArray();
            }
        }
        static void AddColumns(ExcelWorksheet worksheet, int startIndex, List<string> columnNames)
        {
            int columnIndex = startIndex;
            foreach (string columnName in columnNames)
            {
                worksheet.Cells[2, columnIndex].Value = columnName;
                columnIndex++;
            }
        }
        public static List<string> CharacterReplace(List<string> characters)
        {
            var replacedCharacters = new List<string>();
            foreach (var character in characters)
            {
                var replaced = character
                      .Replace(",", "")
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
                      .ToLower();
                replacedCharacters.Add(replaced);
            }
            return replacedCharacters;
        }
        public static string OneCharacterReplace(string character)
        {
            var replaced = character
                     .Replace(",", "")
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
                     .ToLower();
            return replaced;
        }
        public static void OneCharacterReplaceVoid(string character)
        {
            var replaced = character
                     .Replace(",", "")
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
                     .ToLower();
           
        }
        public static List<Product> GetUniqueProductsByGroupCode(List<Product> products)
        {
            return products
                .GroupBy(p => p.GroupCode)     // GroupCode'a göre gruplama
                .Select(g => g.First())        // Her gruptan ilk ürünü seçme
                .ToList();                     // Sonuçları liste olarak döndürme
        }


    }

}

