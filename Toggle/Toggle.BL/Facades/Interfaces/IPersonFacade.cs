using Common.Models.PersonModels;

namespace Toggle.BL.Facades.Interfaces
{
    public interface IPersonFacade : IFacade<PersonDetailModel, PersonListModel, PersonCreateModel, PersonUpdateModel>
    {
        Task AddActivity(Guid personId, Guid activityId);
        Task AddProject(Guid personId, Guid projectId);
    }
}
