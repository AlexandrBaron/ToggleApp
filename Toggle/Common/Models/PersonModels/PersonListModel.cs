namespace Common.Models.PersonModels
{
    public class PersonListModel : IModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
