using ElPlatform.Shared.Models;
using ElPlatform.Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElPlatform.Client.Services.Interfaces
{
    public interface IMediaHttpService
    {
        Task<ApiResponse<PagedList<MediaItemObjVM>>> GetMediaItemsAsync(int page, int pageSize);
        Task<ApiResponse<PagedList<MediaTypeObjVM>>> GetMediaTypesAsync(int page, int pageSize);
        Task<ApiResponse<PagedList<MediaCategoryObjVM>>> GetMediaCategoriesAsync(int page, int pageSize);
        Task<ApiResponse> DeleteMediaTypeAsync(int Id);
        Task<ApiResponse<MediaTypeObjVM>> AddMediaTypeAsync(MediaTypeRequest model);
        Task<ApiResponse<MediaTypeObjVM>> UpdateMediaTypeAsync(MediaTypeRequest model);
        Task<ApiResponse> DeleteMediaCategoryAsync(int Id);
        Task<ApiResponse<MediaCategoryObjVM>> AddMediaCategoryAsync(MediaCategoryRequestVM model);
        Task<ApiResponse<MediaCategoryObjVM>> UpdateMediaCategoryAsync(MediaCategoryRequestVM model);
        Task<ApiResponse<List<MediaCategoryObjVM>>> GetMainMICategoriesAsync();
    }
}
