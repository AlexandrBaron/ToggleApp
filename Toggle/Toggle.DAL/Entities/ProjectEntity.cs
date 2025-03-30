namespace Toggle.DAL.Entities
{
    public class ProjectEntity : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<ActivityEntity> Activities { get; set; }
        public List<PersonEntity> People { get; set; }
    }
}
