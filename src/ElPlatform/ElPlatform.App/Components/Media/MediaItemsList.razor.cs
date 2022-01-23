using ElPlatform.Client.Services.Exceptions;
using ElPlatform.Client.Services.Interfaces;
using ElPlatform.Shared.Models;
using ElPlatform.Shared.Responses;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElPlatform.App.Components
{
    public partial class MediaItemsList
    {
        [Inject]
        public IMediaHttpService MediaService { get; set; }
        private bool _isBusy = false;
        private string _errorMessage = string.Empty;
        private int _pageNumber=1;
        private int _totalPages = 1;
        private int _pageSize = 10;
        private List<MediaItemObjVM> _mediaItems = new();
        public async Task<PagedList<MediaItemObjVM>> GetMediaItemsAsync(int pageNumb=1,int pageSize = 10)
        {
            _isBusy = true;
            try
            {
                var result = await MediaService.GetMediaItemsAsync(pageNumb = 1, pageSize = 10);
                _mediaItems = result.Value.Records.ToList();
                _pageNumber = result.Value.Page;
                _pageSize = result.Value.PageSize;
                _totalPages = result.Value.TotalPages;
                _isBusy = false;
                return result.Value;
            }
            catch(ApiException ex)
            {
                _errorMessage = ex.ApiErrorResponse.Message;
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
            }
           
            _isBusy = false;
            return null;
        }

    }
}
