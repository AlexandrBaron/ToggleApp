
namespace Common.Models.PersonModels
{
    public class PersonUpdateModel : IModel, IModelId
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhotoUrl { get; set; }
    }
}
