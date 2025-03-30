namespace Toggle.DAL.Entities
{
    public class PersonEntity : IEntity
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhotoUrl { get; set; }
        public List<ActivityEntity> Activities { get; set; }
        public List<ProjectEntity> Projects { get; set; }
    }
}
