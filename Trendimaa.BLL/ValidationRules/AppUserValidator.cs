using FluentValidation;
using Trendeimaa.Entities;

namespace Trendimaa.BLL.ValidationRules
{
    public class AppUserValidator: AbstractValidator<AppUser>
    {
        public AppUserValidator() 
        { 
        }
    }
}
