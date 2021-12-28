using ElPlatform.Client.Services.Exceptions;
using ElPlatform.Client.Services.Interfaces;
using ElPlatform.Shared.Models;
using ElPlatform.Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ElPlatform.Client.Services
{
    public class MediaService : IMediaHttpService
    {
        private readonly HttpClient _client;
        public MediaService(HttpClient client)
        {
            _client = client;
        }
        public async Task<ApiResponse<PagedList<MediaItemObjVM>>> GetMediaItemsAsync(int page, int pageSize)
        {
            var response = await _client.GetAsync($"/api/Media/GetMItems?pageNumber={page}&pageSize={pageSize}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ApiResponse<PagedList<MediaItemObjVM>>>();
                return result;
            }
            else
            {
                var errorResult = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new ApiException(errorResult, response.StatusCode);
            }
        }
        public async Task<ApiResponse<PagedList<MediaTypeObjVM>>> GetMediaTypesAsync(int page, int pageSize)
        {
            var response = await _client.GetAsync($"/api/MediaType/GetMTypes?pageNumber={page}&pageSize={pageSize}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ApiResponse<PagedList<MediaTypeObjVM>>>();
                return result;
            }
            else
            {
                var errorResult = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new ApiException(errorResult, response.StatusCode);
            }
        }
        public async Task<ApiResponse<PagedList<MediaCategoryObjVM>>> GetMediaCategoriesAsync(int page, int pageSize)
        {
            var response = await _client.GetAsync($"/api/Category/GetMICatigories?pageNumber={page}&pageSize={pageSize}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ApiResponse<PagedList<MediaCategoryObjVM>>>();
                return result;
            }
            else
            {
                var errorResult = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new ApiException(errorResult, response.StatusCode);
            }
        }
        public async Task<ApiResponse> DeleteMediaTypeAsync(int Id)
        {
            var response = await _client.DeleteAsync($"/api/MediaType/{Id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ApiResponse>();
                return result;
            }
            else
            {
                var errorResult = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new ApiException(errorResult, response.StatusCode);
            }
        }
        public async Task<ApiResponse<MediaTypeObjVM>> AddMediaTypeAsync(MediaTypeRequest model)
        {
            var response = await _client.PostAsJsonAsync($"/api/MediaType/",model);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ApiResponse<MediaTypeObjVM>>();
                return result;
            }
            else
            {
                var errorResult = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new ApiException(errorResult, response.StatusCode);
            }
        }

        public async Task<ApiResponse<MediaTypeObjVM>> UpdateMediaTypeAsync(MediaTypeRequest model)
        {
            var response = await _client.PutAsJsonAsync($"/api/MediaType/", model);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ApiResponse<MediaTypeObjVM>>();
                return result;
            }
            else
            {
                var errorResult = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new ApiException(errorResult, response.StatusCode);
            }
        }

        public async Task<ApiResponse> DeleteMediaCategoryAsync(int Id)
        {
            var response = await _client.DeleteAsync($"/api/Category/{Id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ApiResponse>();
                return result;
            }
            else
            {
                var errorResult = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new ApiException(errorResult, response.StatusCode);
            }
        }

        public async Task<ApiResponse<MediaCategoryObjVM>> AddMediaCategoryAsync(MediaCategoryRequestVM model)
        {
            var response = await _client.PostAsJsonAsync($"/api/Category/", model);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ApiResponse<MediaCategoryObjVM>>();
                return result;
            }
            else
            {
                var errorResult = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new ApiException(errorResult, response.StatusCode);
            }
        }

        public async Task<ApiResponse<MediaCategoryObjVM>> UpdateMediaCategoryAsync(MediaCategoryRequestVM model)
        {
            var response = await _client.PutAsJsonAsync($"/api/Category/", model);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ApiResponse<MediaCategoryObjVM>>();
                return result;
            }
            else
            {
                var errorResult = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new ApiException(errorResult, response.StatusCode);
            }
        }

        public async Task<ApiResponse<List<MediaCategoryObjVM>>> GetMainMICategoriesAsync()
        {
            var response = await _client.GetAsync($"/api/Category/GetMIMCatigories");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ApiResponse<List<MediaCategoryObjVM>>>();
                return result;
            }
            else
            {
                var errorResult = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new ApiException(errorResult, response.StatusCode);
            }
        }
    }
}
