using ElPlatform.Client.Services.Exceptions;
using ElPlatform.Client.Services.Interfaces;
using ElPlatform.Shared.Models;
using ElPlatform.Shared.Responses;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElPlatform.App.Components
{
    public partial class MediaCategoryList
    {
        [Inject]
        public IMediaHttpService MediaService { get; set; }

        [Inject]
        IDialogService DialogService { get; set; }
        public bool _isBusy = false;
        private string _errorMessage = string.Empty;
        private int _pageNumber = 1;
        private int _totalPages = 1;
        private int _pageSize = 10;
        private MediaCategoryRequestVM _model = new();
        public bool _isEdit = false;
        private bool visible = false;
        private List<MediaCategoryObjVM> _mediaCategories = new();
        public List<MediaCategoryObjVM> _mainCategories = new();
        public async Task<PagedList<MediaCategoryObjVM>> GetMediaCategoriesAsync(int pageNumb = 1, int pageSize = 10)
        {
            _isBusy = true;
            try
            {
                var result = await MediaService.GetMediaCategoriesAsync(pageNumb = 1, pageSize = 10);
                _mediaCategories = result.Value.Records.ToList();
                _pageNumber = result.Value.Page;
                _pageSize = result.Value.PageSize;
                _totalPages = result.Value.TotalPages;
                return result.Value;
            }
            catch (ApiException ex)
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
        protected async override Task OnInitializedAsync()
        {
            var result = await MediaService.GetMediaCategoriesAsync(_pageNumber = 1, _pageSize = 10);
            _mediaCategories = result.Value.Records;
            var rec = await GetMainCategory();
            _mainCategories = rec;
        }
        private void OpenDialog()
        {
            _isEdit = false;
            _model = new();
            visible = true;
        }
        public async Task<ApiResponse> DeleteMediaCategoryAsync(int Id)
        {
            _isBusy = true;
            try
            {
                var result = await MediaService.DeleteMediaCategoryAsync(Id);
                if (result.IsSuccess)
                {
                    var items = await MediaService.GetMediaCategoriesAsync(_pageNumber = 1, _pageSize = 10);
                    _mediaCategories = items.Value.Records;
                }
                return result;
            }
            catch (ApiException ex)
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
        private async Task AddMediaCategoryAsync()
        {
            _isBusy = true;
            _errorMessage = string.Empty;
            try
            {
                await MediaService.AddMediaCategoryAsync(_model);
                visible = false;
            }
            catch (ApiException ex)
            {
                ///todo: Log this errors
                _errorMessage = ex.ApiErrorResponse.Message;
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
            }
            _isBusy = false;
        }
        private async Task UpdateMediaCategoryAsync()
        {
            _isBusy = true;
            _errorMessage = string.Empty;
            try
            {
                await MediaService.UpdateMediaCategoryAsync(_model);
                visible = false;
            }
            catch (ApiException ex)
            {
                ///todo: Log this errors
                _errorMessage = ex.ApiErrorResponse.Message;
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
            }
            _isBusy = false;
        }

        private async Task<List<MediaCategoryObjVM>> GetMainCategory()
        {

            _isBusy = true;
            try
            {
                _mainCategories.Add(new() { Id = 0, NameEn = "Select", NameAr = "أختر" });
                var result = await MediaService.GetMainMICategoriesAsync();
                _mainCategories.AddRange( result.Value);
                return _mainCategories;
            }
            catch (ApiException ex)
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
        private void EditItem(MediaCategoryObjVM item, bool isEdit)
        {
            _model.Id = item.Id;
            _model.NameAr = item.NameAr;
            _model.NameEn = item.NameEn;
            _model.IsActive = item.IsActive;
            _model.ParantId = Convert.ToInt32(item.ParantId);
            _isEdit = isEdit;
            visible = true;
        }
    }
}
