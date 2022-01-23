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
    public partial class MediaList
    {
        [Inject]
        public IMediaHttpService MediaService { get; set; }
        [Inject]
        public NavigationManager Navigation { get; set; }
        [Inject]
        IDialogService DialogService { get; set; }
        public bool _isBusy = false;
        public bool _isEdit = false;
        private string _errorMessage = string.Empty;
        private int _pageNumber = 1;
        private int _totalPages = 1;
        private int _pageSize = 10;
        private int _selectedMainCategory = 0;
        private bool visible = false;
        private bool viewvisible = false;
        private string _viewdURL = string.Empty;
        private MediaItemRequest _model = new();
        private List<MediaItemObjVM> _mediaItems = new();
        private List<MediaItemObjVM> _items = new();
        private List<MediaTypeObjVM> _mediaTypes = new();
        private List<MediaCategoryObjVM> _mediaMainCategories = new();
        private List<MediaCategoryObjVM> _mediaSubCategories = new();
        public async Task<PagedList<MediaItemObjVM>> GetMediaItemsAsync(int pageNumb = 1, int pageSize = 10)
        {
            _isBusy = true;
            try
            {
                var result = await MediaService.GetMediaItemsAsync(pageNumb = 1, pageSize = 10);
                _items = result.Value.Records.ToList();
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
       public async Task<List<MediaTypeObjVM>> GetMediaTypiesAsync()
        {
           
            try
            {
                _mediaTypes.Add(new() { Id = 0, NameEn = "Select", NameAr = "أختر" });
                var result = await MediaService.GetAllTypesAsync();
                _mediaTypes.AddRange(result.Value);
                return _mediaTypes;
            }
            catch (ApiException ex)
            {
                _errorMessage = ex.ApiErrorResponse.Message;
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
            }
            
            return null;
        }
        public async Task<List<MediaCategoryObjVM>> GetMediaMainCategoriesAsync()
        {

            try
            {
                _mediaMainCategories.Add(new() { Id = 0, NameEn = "Select", NameAr = "أختر" });
                var result = await MediaService.GetMainMICategoriesAsync();
                _mediaMainCategories.AddRange(result.Value);
                return _mediaMainCategories;
            }
            catch (ApiException ex)
            {
                _errorMessage = ex.ApiErrorResponse.Message;
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
            }

            return null;
        }
        public async Task<List<MediaCategoryObjVM>> GetMediaSubCategoriesAsync()
        {

            try
            {
                _mediaSubCategories.Add(new() { Id = 0, NameEn = "Select", NameAr = "أختر" });
                var result = await MediaService.GetMainMISCategoriesAsync(_selectedMainCategory);
                _mediaSubCategories.AddRange(result.Value);
                return _mediaSubCategories;
            }
            catch (ApiException ex)
            {
                _errorMessage = ex.ApiErrorResponse.Message;
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
            }

            return null;
        }
        async Task OnMainCategorySelected(HashSet<int> values)
        {
            _mediaSubCategories = new();
            _mediaSubCategories.Add(new() { Id = 0, NameEn = "Select", NameAr = "أختر" });
            var result =await  MediaService.GetMainMISCategoriesAsync(_selectedMainCategory);
            _mediaSubCategories.AddRange(result.Value);
        }
        
        public async Task<ApiResponse> DeleteMediaItemAsync(int Id)
        {
            _isBusy = true;
            try
            {
                var result = await MediaService.DeleteMediaItemAsync(Id);
                if (result.IsSuccess)
                {
                    var items = await MediaService.GetMediaItemsAsync(_pageNumber = 1, _pageSize = 10);
                    _mediaItems = items.Value.Records;
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
        private void ViewMediaItem(string URL)
        {
            _viewdURL = URL;
            viewvisible = true;

        }

        private async Task AddMediaItemAsync()
        {
            _isBusy = true;
            _errorMessage = string.Empty;
            try
            {
                await MediaService.AddMediaItemAsync(_model);
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
        private async Task UpdateMediaItemAsync()
        {
            _isBusy = true;
            _errorMessage = string.Empty;
            try
            {
                await MediaService.UpdateMediaItemAsync(_model);
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
        protected async override Task OnInitializedAsync()
        {
            var result = await MediaService.GetMediaItemsAsync(_pageNumber = 1, _pageSize = 10);
            _mediaItems = result.Value.Records;
            var types = await GetMediaTypiesAsync();
            _mediaTypes = types;
            var mainCategories = await GetMediaMainCategoriesAsync();
            _mediaMainCategories = mainCategories;
        }
        private void OpenDialog()
        {
            _isEdit = false;
            _model = new();
            visible = true;
        }
        private void EditItem(MediaItemObjVM item, bool isEdit)
        {
            _model.Id = item.Id;
            _model.TitleAr = item.TitleAr;
            _model.TitleEn = item.TitleEn;
            _model.URL = item.URL;
            _model.DescriptionAr = item.DescriptionAr;
            _model.DescriptionEn = item.DescriptionEn;
            _model.IsActive = item.IsActive;
            _model.MediaTypeId = item.TypeId;
            _model.MediaCategoryId = item.CategoryId;
            _model.IsPublished = item.IsPublished;
            _isEdit = isEdit;
            visible = true;
        }
    }
}
