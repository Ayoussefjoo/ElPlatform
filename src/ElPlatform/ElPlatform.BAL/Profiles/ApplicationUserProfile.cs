using AutoMapper;
using ElPlatform.BAL.ViewModels;
using ElPlatform.DAL.Model;

namespace ElPlatform.BAL.Profiles
{
    public class ApplicationUserProfile : Profile
    {
        public ApplicationUserProfile()
        {
            CreateMap<ApplicationUser,ApplicationUserVM>();
            CreateMap<ApplicationUserVM, ApplicationUser>();
        }

    }
}
