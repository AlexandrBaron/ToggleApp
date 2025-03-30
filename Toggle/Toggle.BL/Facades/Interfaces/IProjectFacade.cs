using Common.Models.ProjectModels;

namespace Toggle.BL.Facades.Interfaces
{
    public interface IProjectFacade : IFacade<ProjectDetailModel, ProjectListModel, ProjectCreateModel, ProjectUpdateModel>
    {
        Task AddActivityToProject(Guid personId, Guid activityId);
        Task AddActivityToProject2(Guid projectId, Guid activityId);
    }
}
