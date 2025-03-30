namespace Common.Models.ActivityModels
{
    public class ActivityCreateModel : IModel
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
    }
}
