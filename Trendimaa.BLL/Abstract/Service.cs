using FluentValidation;
using Trendeimaa.Entities.Interface;
using Trendimaa.BLL.Extension;
using Trendimaa.BLL.Interface;
using Trendimaa.Common;
using Trendimaa.DAL.Context;
using Trendimaa.DAL.UnitOfWork;

namespace Trendimaa.BLL.Abstract
{
    public class Service<Entity> : IService<Entity>
 where Entity : BaseEntity, new()
    {
        private readonly IValidator<Entity> _validator;
        private readonly IUOW _uow;
        private readonly TrendimaaContext _context;
        public Service(IValidator<Entity> validator, IUOW uow, TrendimaaContext context)
        {
            _uow = uow;
            _validator = validator;
            _context = context;
        }


        public async Task<IResponse<Entity>> CreateAsync(Entity entity)
        {
            var result = await _validator.ValidateAsync(entity);
            if (result.IsValid)
            {
                var created = await _uow.GetRepository<Entity>().CreateAsync(entity);


                return new Response<Entity>(ResponseType.Success, created);
            }
            return new Response<Entity>(entity, result.ConvertToCustomValidationError());

        }


        public async Task<IResponse<List<Entity>>> GetAllAsync()
        {
            var entities = await _uow.GetRepository<Entity>().GetAllAsync();
            return new Response<List<Entity>>(ResponseType.Success, entities);
        }


        public async Task<IResponse<Entity>>? GetByIdAsync(int id)
        {
            var entity = await _uow.GetRepository<Entity>().FindAsync(id);
            if (entity == null)
            {
                return new Response<Entity>(ResponseType.NotFound, "Data bulunamadı");
            }
            return new Response<Entity>(ResponseType.Success, entity);
        }


        public async Task<IResponse> RemoveAsync(int id)
        {
            var entity = await _uow.GetRepository<Entity>().FindAsync(id);
            if (entity == null)
            {
                return new Response<Entity>(ResponseType.NotFound, "Data bulunamadı");
            }
            _uow.GetRepository<Entity>().Remove(entity);
            await _uow.SaveChangesAsync();
            return new Response(ResponseType.Success);
        }

        public async Task<IResponse<List<Entity>>> CreateRangeAsync(List<Entity> entities)
        {
            foreach (var entity in entities)
            {
                var result = await _validator.ValidateAsync(entity);
                if (!result.IsValid)
                    return new Response<List<Entity>>(entities, result.ConvertToCustomValidationError());
            }
            await _uow.GetRepository<Entity>().CreateRangeAsync(entities);
            return new Response<List<Entity>>(ResponseType.Success, entities);
        }

        public async Task<Entity> UpdateAsyncNotResponse(Entity changed)
        {

            var unchanged = await _uow.GetRepository<Entity>().FindAsync(changed.Id);

            _uow.GetRepository<Entity>().Update(changed, unchanged);
            await _uow.SaveChangesAsync();

            return changed;

        }

        public async Task<IResponse<Entity>> UpdateAsync(Entity changed)
        {
            var result = await _validator.ValidateAsync(changed);
            if (result.IsValid)
            {
                var unchanged = await _uow.GetRepository<Entity>().FindAsync(changed.Id);
                if (unchanged == null)
                {
                    new Response<Entity>(ResponseType.NotFound, "Data bulunamadı");
                }
                _uow.GetRepository<Entity>().Update(changed, unchanged);
                await _uow.SaveChangesAsync();
                return new Response<Entity>(ResponseType.Success, changed);
            }
            return new Response<Entity>(changed, result.ConvertToCustomValidationError());
        }


    }
}
