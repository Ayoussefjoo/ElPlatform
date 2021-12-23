using ElPlatform.BAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElPlatform.BAL.IServices
{
    public interface IMediaService
    {
        // media item services
        Task<List<MediaItemVM>>  GetMediaItemsAsync();
        Task<MediaItemVM> GetMediaItemByIdAsync(int Id);
        Task<MediaItemVM> AddMediaItemAsync(MediaItemRequestVM model);
        Task DeleteMediaItemAsync(int Id);
        Task<MediaItemVM> UpdateMediaItemAsync(MediaItemRequestVM model);

        //media types Services
        Task<List<MediaTypeVM>> GetMediaTypesAsync();
        Task<MediaTypeVM> GetMediaTypeByIdAsync(int Id);
        Task<MediaTypeVM> AddMediaTypeAsync(MediaTypeVM model);
        Task DeleteMediaTypeAsync(int Id);
        Task<MediaTypeVM> UpdateMediaTypeAsync(MediaTypeVM model);

        //media item category
        Task<List<MediaItemCategoryVM>> GetMediaItemCategoriesAsync();
        Task<MediaItemCategoryVM> GetMediaItemCategoryByIdAsync(int Id);
        Task<MediaItemCategoryVM> AddMediaItemCategoryAsync(MediaItemCategoryVM model);
        Task DeleteMediaItemCategoryAsync(int Id);
        Task<MediaItemCategoryVM> UpdateMediaItemCategoryAsync(MediaItemCategoryVM model);
    }
}
