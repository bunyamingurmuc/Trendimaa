using Trendeimaa.Entities.Interface;
using Trendimaa.Common;

namespace Trendimaa.BLL.Interface
{
    public interface IService<Entity>
        where Entity : BaseEntity, new()
    {
        Task<IResponse<List<Entity>>> GetAllAsync();
        Task<IResponse<Entity>>? GetByIdAsync(int id);
        Task<IResponse<Entity>> CreateAsync(Entity entity);
        Task<IResponse<Entity>> UpdateAsync(Entity changed);
        Task<IResponse> RemoveAsync(int id);
        Task<IResponse<List<Entity>>> CreateRangeAsync(List<Entity> entities);
        Task<Entity> UpdateAsyncNotResponse(Entity changed);

    }
}
