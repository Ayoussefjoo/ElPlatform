using ElPlatform.BAL.Options;
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
        Task<PagedList<MediaItemVM>>  GetMediaItemsAsync(int page, int pageSize);
        Task<MediaItemVM> GetMediaItemByIdAsync(int Id);
        Task<MediaItemVM> AddMediaItemAsync(MediaItemRequestVM model);
        Task DeleteMediaItemAsync(int Id);
        Task<MediaItemVM> UpdateMediaItemAsync(MediaItemRequestVM model);

        //media types Services
        Task<PagedList<MediaTypeVM>> GetMediaTypesAsync(int page, int pageSize);
        Task<MediaTypeVM> GetMediaTypeByIdAsync(int Id);
        Task<MediaTypeVM> AddMediaTypeAsync(MediaTypeRequestVM model);
        Task DeleteMediaTypeAsync(int Id);
        Task<MediaTypeVM> UpdateMediaTypeAsync(MediaTypeVM model);

        //media item category
        Task<PagedList<MediaItemCategoryVM>> GetMediaItemCategoriesAsync(int page, int pageSize);
        Task<MediaItemCategoryVM> GetMediaItemCategoryByIdAsync(int Id);
        Task<MediaItemCategoryVM> AddMediaItemCategoryAsync(MediaItemCategoryVM model);
        Task DeleteMediaItemCategoryAsync(int Id);
        Task<MediaItemCategoryVM> UpdateMediaItemCategoryAsync(MediaItemCategoryVM model);
        Task<List<MediaItemCategoryVM>> GetMediaItemMainCategoriesAsync();
    }
}
