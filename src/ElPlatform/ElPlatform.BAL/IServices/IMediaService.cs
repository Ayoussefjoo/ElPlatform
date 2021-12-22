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
        Task<List<MediaItemVM>>  GetMediaItemsAsync();
        Task<MediaItemVM> GetMediaItemByIdAsync(int Id);
        Task<string> AddMediaItem(int model);
        Task<string> DeleteMediaItem(int Id);
        Task<string> UpdateMediaItem(int Id);

        //media Type Services
        Task<List<MediaTypeVM>> GetMediaTypesAsync();
        Task<MediaTypeVM> GetMediaTypeByIdAsync(int Id);
    }
}
