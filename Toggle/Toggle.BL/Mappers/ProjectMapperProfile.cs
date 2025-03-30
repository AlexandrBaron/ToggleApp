using AutoMapper;
using Common.Models.ProjectModels;
using Toggle.DAL.Entities;
namespace Toggle.BL.Mappers
{
    public class ProjectMapperProfile : Profile
    {
        public ProjectMapperProfile()
        {
            CreateMap<ProjectEntity, ProjectDetailModel>();
            CreateMap<ProjectEntity, ProjectListModel>();

            CreateMap<ProjectCreateModel, ProjectEntity>()
                .ForMember(pr => pr.Id, expression => expression.Ignore());
            CreateMap<ProjectUpdateModel, ProjectEntity>();
        }
    }
}
