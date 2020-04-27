using AutoMapper;
using Insurance.Api.ViewModels;
using Insurance.Models.Content;

namespace Insurance.Api.Profiles
{
    public class ItemProfile : Profile
    {
        public ItemProfile()
        {
            CreateMap<Item, ItemViewModel>().ReverseMap();
        }
    }
}
