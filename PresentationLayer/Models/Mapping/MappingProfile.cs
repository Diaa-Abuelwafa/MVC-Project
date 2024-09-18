using AutoMapper;
using DataAccessLayer.Models;

namespace PresentationLayer.Models.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Just Example
            CreateMap<Employee, Department>().ReverseMap();
        }
    }
}
