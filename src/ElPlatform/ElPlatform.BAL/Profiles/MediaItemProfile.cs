using AutoMapper;
using ElPlatform.BAL.ViewModels;
using ElPlatform.DAL.Model;
using System.Collections.Generic;

namespace ElPlatform.BAL.Profiles
{
    public class MediaItemProfile : Profile
    {
        public MediaItemProfile()
        {
            CreateMap<MediaItem,MediaItemVM>();
            CreateMap<MediaItemVM, MediaItem>();
            CreateMap<List<MediaItem> , List<MediaItemVM>>();
            CreateMap<List<MediaItemVM>, List<MediaItem>>();

            CreateMap<MediaType, MediaTypeVM>();
            CreateMap<MediaTypeVM, MediaType>();
            CreateMap<List<MediaType>, List<MediaTypeVM>>();
            CreateMap<List<MediaTypeVM>, List<MediaType>>();
        }

    }
}
