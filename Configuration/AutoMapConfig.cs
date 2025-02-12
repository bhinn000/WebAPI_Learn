using AutoMapper;
using WebAPI_Learn.Models;
using WebAPI_Learn.Data;
using Microsoft.IdentityModel.Tokens;

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
            CreateMap<StudentData, StudentDTO>().ReverseMap(); //this works only when there is similar named propeties in both class

            //this works  when there is different named propeties in both class (Name and StudentName)
            //CreateMap<StudentData, StudentDTO>().ForMember(n => n.StudentName, opt => opt.MapFrom(x => x.Name)).ReverseMap();

            //to ignore specific property to get mapped
            //CreateMap<StudentData, StudentDTO>().ReverseMap().ForMember(n => n.StudentName, opt => opt.Ignore());
            //CreateMap<StudentData, StudentDTO>().ForMember(n => n.StudentName, opt => opt.Ignore()).ReverseMap();

            //to transform
            //CreateMap<StudentDTO, StudentData>().ReverseMap().AddTransform<string>(n=>string.IsNullOrEmpty(n)?"No address found":n); //if we do this when the value for address is actually null, it gives the response to "null"
            CreateMap<StudentDTO, StudentData>().ReverseMap().ForMember(n => n.Address, opt=>opt.MapFrom(n=> string.IsNullOrEmpty(n.Address) ? "No address found" : n.Address));
          
        }
    }
}
