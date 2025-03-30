namespace Common.Models.ProjectModels
{
    public class ProjectUpdateModel : IModel, IModelId
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
