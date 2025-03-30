using AutoMapper;
using Common.Models.PersonModels;
using Toggle.BL.Facades.Interfaces;
using Toggle.DAL.Entities;
using Toggle.DAL.Repositories;

namespace Toggle.BL.Facades
{
    public class PersonFacade(Repository<PersonEntity> repository, IMapper mapper, Repository<ActivityEntity> activityRepository, Repository<ProjectEntity> projectRepository) : FacadeBase<PersonEntity, PersonDetailModel, PersonListModel, PersonCreateModel, PersonUpdateModel>(repository, mapper), IPersonFacade
    {
        public async Task AddActivity(Guid personId, Guid activityId)
        {

            var person = await repository.Get(personId);
            if (person == null)
            {
                throw new ArgumentException("Person with this Id does not exists");
            }
            var activity = await activityRepository.Get(activityId);

            if (activity == null)
            {
                throw new ArgumentException("Project with this Id does not exists");
            }

            person.Activities.Add(activity);

            await repository.Update(person);
        }
        public async Task AddProject(Guid personId, Guid projectId)
        {

            var person = await repository.Get(personId);
            if (person == null)
            {
                throw new ArgumentException("Person with this Id does not exists");
            }

            var project = await projectRepository.Get(projectId);
            if (project == null)
            {
                throw new ArgumentException("Project with this Id does not exists");
            }

            person.Projects.Add(project);

            await repository.Update(person);
        }
    }
}
