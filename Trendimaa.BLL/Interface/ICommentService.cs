using Trendeimaa.Entities;
using Trendimaa.Common;
using Trendimaa.DTO;

namespace Trendimaa.BLL.Interface
{
    public interface ICommentService:IService<Comment>
    {
        Task<IResponse<List<CommentDTO>>> GetProductComments(int productId);
        Task<IResponse<List<CommentDTO>>> GetSellerComments(int sellerId);
    }
}
