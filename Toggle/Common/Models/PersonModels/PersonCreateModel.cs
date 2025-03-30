
namespace Common.Models.PersonModels
{
    public class PersonCreateModel : IModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhotoUrl { get; set; }
    }
}
