using AutoMapper;
using Insurance.Api.ViewModels;
using Insurance.Models.Content;

namespace Insurance.Api.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryViewModel>();
        }
    }
}
