using AutoMapper;
using Common.Models.ProjectModels;
using Toggle.BL.Facades.Interfaces;
using Toggle.DAL.Entities;
using Toggle.DAL.Repositories;

namespace Toggle.BL.Facades
{
    public class ProjectFacade(Repository<ProjectEntity> repository, IMapper mapper, Repository<ActivityEntity> activityRepository) : FacadeBase<ProjectEntity, ProjectDetailModel, ProjectListModel, ProjectCreateModel, ProjectUpdateModel>(repository, mapper), IProjectFacade
    {
        public async Task AddActivityToProject(Guid projectId, Guid activityId)
        {
            var project = await repository.Get(projectId);
            if (project == null)
            {
                throw new ArgumentException("Project with this Id does not exists");
            }
            var activity = await activityRepository.Get(activityId);

            if (activity == null)
            {
                throw new ArgumentException("Activity with this Id does not exists");
            }

            project.Activities.Add(activity);

            await repository.Update(project);
        }
        public async Task AddActivityToProject2(Guid projectId, Guid activityId)
        {
            var project = await repository.Get(projectId);
            if (project == null)
            {
                throw new ArgumentException("Project with this Id does not exist");
            }

            var activity = await activityRepository.Get(activityId);
            if (activity == null)
            {
                throw new ArgumentException("Activity with this Id does not exist");
            }

            if (!project.Activities.Any(a => a.Id == activityId))
            {
                project.Activities.Add(activity);
            }

            await repository.Update(project);
        }

    }
}
