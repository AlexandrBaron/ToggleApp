using Common.Models.ActivityModels;
using Common.Models.ProjectModels;

namespace Common.Models.PersonModels
{
    public class PersonDetailModel : IModel, IModelId
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhotoUrl { get; set; }
        public List<ActivityListModel> Activities { get; set; }
        public List<ProjectListModel> Projects { get; set; }
    }
}
