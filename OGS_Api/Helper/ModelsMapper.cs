using AutoMapper;
using OGS_Api.Data;
using OGS_Api.DTO;

namespace OGS_Api.Helper
{
    public class ModelsMapper : Profile
    {
        
        public ModelsMapper()
        {
            CreateMap<User, UserModel>().ReverseMap();
            CreateMap<Categories, CategoryFileModel>().ReverseMap();
            CreateMap<Product, ProductFileModel>().ReverseMap();
            CreateMap<Orders, OrderStatusModel>().ReverseMap();
            CreateMap<User, UserFileModel>().ReverseMap();
        }
    }
}