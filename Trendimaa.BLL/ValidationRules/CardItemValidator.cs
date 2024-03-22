using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trendeimaa.Entities;

namespace Trendimaa.BLL.ValidationRules
{
    public class CardItemValidator:AbstractValidator<CardItem>
    {
        public CardItemValidator()
        {
            
        }
    }
}
