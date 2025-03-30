using AutoMapper;
using Common.Models.PersonModels;
using Toggle.DAL.Entities;
namespace Toggle.BL.Mappers
{
    public class PersonMapperProfile : Profile
    {
        public PersonMapperProfile()
        {
            CreateMap<PersonEntity, PersonDetailModel>();
            CreateMap<PersonEntity, PersonListModel>();

            CreateMap<PersonCreateModel, PersonEntity>()
                .ForMember(p => p.Id, expression => expression.Ignore());
            CreateMap<PersonUpdateModel, PersonEntity>();
        }
    }
}
