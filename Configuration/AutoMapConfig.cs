using AutoMapper;
using WebAPI_Learn.Models;
using WebAPI_Learn.Data;

namespace WebAPI_Learn.Configuration
{
    //In AutoMapper, AutoMapConfig (or a similar configuration class) is essential because it defines the rules for how one type
    //should be converted into another type.This is known as mapping configuration.
    //IMapper is the interface that executes the actual conversion process.
    //typeof(AutoMapConfig) : to get metadata about the class type, AddAutoMapper needs to know where to look for configuration

    public class AutoMapConfig : Profile
    {
        public AutoMapConfig()
        {
            CreateMap<StudentData, StudentDTO>().ReverseMap();
        }
    }
}
