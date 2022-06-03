using AutoMapper;
using lab.WebApi19Sample.EntityModels;
using lab.WebApi19Sample.ViewModels;

namespace lab.WebApi19Sample.Mapper
{
    public class DefaultMappingProfile : Profile
    {
        public DefaultMappingProfile()
        {
            CreateMap<StudentViewModel, Student>();
            CreateMap<Student, StudentViewModel>();
        }
    }
}
