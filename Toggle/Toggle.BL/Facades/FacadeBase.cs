using AutoMapper;
using Common.Models;
using Toggle.BL.Facades.Interfaces;
using Toggle.DAL.Entities;
using Toggle.DAL.Repositories;

namespace Toggle.BL.Facades
{
    public class FacadeBase<TEntity, TDetailModel, TListModel, TCreateModel, TUpdateModel> : IFacade<TDetailModel, TListModel, TCreateModel, TUpdateModel>
        where TEntity : class, IEntity
        where TDetailModel : class, IModel, IModelId
        where TListModel : class, IModel
        where TCreateModel : class, IModel
        where TUpdateModel : class, IModel, IModelId
    {
        private readonly IMapper mapper;
        private readonly Repository<TEntity> repository;
        public FacadeBase(Repository<TEntity> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public List<TListModel> GetAll()
        {
            var result = repository.Get();
            var mapedResult = mapper.Map<List<TListModel>>(result);
            return mapedResult.ToList();
        }
        public async Task<TDetailModel?> GetById(Guid id)
        {
            var result = await repository.Get(id);
            if (result is not null)
            {
                var mapedResult = mapper.Map<TDetailModel>(result);
                return mapedResult;
            }
            return null;
        }
        public async Task<bool> DeleteById(Guid id)
        {
            return await repository.Delete(id);
        }
        public async Task<Guid> Create(TCreateModel model)
        {
            try
            {
                var mappedModel = mapper.Map<TEntity>(model);
                return await repository.Create(mappedModel);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
        public async Task Update(TUpdateModel model)
        {
            try
            {
                var existingItem = await repository.Get(model.Id);

                if (existingItem is not null)
                {
                    mapper.Map(model, existingItem);

                    //var mappedModel = mapper.Map<TEntity>(model);
                    await repository.Update(existingItem);
                }
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

    }
}
