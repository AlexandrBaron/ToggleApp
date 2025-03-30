using AutoMapper;
using Common.Models.ActivityModels;
using Toggle.BL.Facades.Interfaces;
using Toggle.DAL.Entities;
using Toggle.DAL.Repositories;

namespace Toggle.BL.Facades
{
    public class ActivityFacade(Repository<ActivityEntity> repository, IMapper mapper) : FacadeBase<ActivityEntity, ActivityDetailModel, ActivityListModel, ActivityCreateModel, ActivityDetailModel>(repository, mapper), IActivityFacade
    {
    }
}
