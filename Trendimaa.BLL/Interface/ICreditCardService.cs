using FluentValidation;
using Trendeimaa.Entities;
using Trendimaa.Common;
using Trendimaa.DTO;

namespace Trendimaa.BLL.Interface
{
    public interface ICreditCardService:IService<CreditCard>
    {
        Task<Response<List<CreditCard>>> GetUserCCs(int id);
    }
}
