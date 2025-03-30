namespace Toggle.DAL.Entities
{
    public class ActivityEntity : IEntity
    {
        public Guid Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
    }
}
