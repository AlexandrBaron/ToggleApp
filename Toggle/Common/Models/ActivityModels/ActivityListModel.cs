namespace Common.Models.ActivityModels
{
    public class ActivityListModel : IModel
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
    }
}
