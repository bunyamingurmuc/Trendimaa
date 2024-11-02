using Trendeimaa.Entities;
using Trendimaa.Common;
using Trendimaa.DTO;
using Trendimaa.DTO.Listing;

namespace Trendimaa.BLL.Interface
{
    public interface ICommentService:IService<Comment>
    {
        Task<IResponse<ListingDTO<CommentDTO>>> GetProductCommentsWithCount(int productId, int page, int quantity);
        Task<IResponse<ListingDTO<CommentDTO>>> GetSellerCommentsWithCount(int sellerId, int page, int quantity);
    }
}
