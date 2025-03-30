using Common.Models.ActivityModels;
using Common.Models.PersonModels;

namespace Common.Models.ProjectModels
{
    public class ProjectDetailModel : IModel, IModelId
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<ActivityListModel> Activities { get; set; }
        public List<PersonListModel> People { get; set; }
    }
}
