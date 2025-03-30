using AutoMapper;
using Common.Models.ActivityModels;
using Toggle.DAL.Entities;
namespace Toggle.BL.Mappers
{
    public class ActivityMapperProfile : Profile
    {
        public ActivityMapperProfile()
        {
            CreateMap<ActivityEntity, ActivityDetailModel>();
            CreateMap<ActivityEntity, ActivityListModel>();

            CreateMap<ActivityCreateModel, ActivityEntity>()
                .ForMember(a => a.Id, expression => expression.Ignore());

            CreateMap<ActivityDetailModel, ActivityEntity>();
        }
    }
}
