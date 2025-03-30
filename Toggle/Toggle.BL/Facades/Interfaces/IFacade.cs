using Common.Models;

namespace Toggle.BL.Facades.Interfaces
{
    public interface IFacade<TDetailModel, TListModel, TCreateModel, TUpdateModel>
        where TDetailModel : class, IModel
        where TListModel : class, IModel
        where TCreateModel : class, IModel
        where TUpdateModel : class, IModel, IModelId
    {
        public List<TListModel> GetAll();
        public Task<TDetailModel?> GetById(Guid id);
        public Task<bool> DeleteById(Guid id);
        public Task<Guid> Create(TCreateModel model);
        public Task Update(TUpdateModel model);
    }
}
