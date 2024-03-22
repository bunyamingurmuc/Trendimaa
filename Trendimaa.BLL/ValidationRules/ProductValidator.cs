using FluentValidation;
using Trendeimaa.Entities;

namespace Trendimaa.BLL.ValidationRules
{
    public class ProductValidator: AbstractValidator<Product>
    {
        public ProductValidator() { }
    }
}
