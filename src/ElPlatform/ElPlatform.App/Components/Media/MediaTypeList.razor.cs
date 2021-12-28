using ElPlatform.Client.Services;
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
    public partial class MediaTypeList
    {
        [Inject]
        public IMediaHttpService MediaService { get; set; }
        [Inject]
        public NavigationManager Navigation { get; set; }
        [Inject]
        IDialogService DialogService { get; set; }
        private MediaTypeRequest _model = new();
        
        public bool _isBusy = false;
        public bool _isEdit = false;
        private string _errorMessage = string.Empty;
        private int _pageNumber = 1;
        private int _totalPages = 1;
        private int _pageSize = 10;
        private bool visible = false;
        private List<MediaTypeObjVM> _mediaTypes = new();
        private List<MediaTypeObjVM> _items = new();
        public async Task<PagedList<MediaTypeObjVM>> GetMediaTypesAsync(int pageNumb = 1, int pageSize = 10)
        {
            _isBusy = true;
            try
            {
                var result = await MediaService.GetMediaTypesAsync(pageNumb = 1, pageSize = 10);
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
        public async Task<ApiResponse> DeleteMediaTypeAsync(int Id)
        {
            _isBusy = true;
            try
            {
                var result = await MediaService.DeleteMediaTypeAsync(Id);
                if (result.IsSuccess)
                {
                   var items= await MediaService.GetMediaTypesAsync(_pageNumber = 1, _pageSize = 10);
                    _mediaTypes = items.Value.Records;
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
        private async Task AddMediaTypeAsync()
        {
            _isBusy = true;
            _errorMessage = string.Empty;
            try
            {
                await MediaService.AddMediaTypeAsync(_model);
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
        private async Task UpdateMediaTypeAsync()
        {
            _isBusy = true;
            _errorMessage = string.Empty;
            try
            {
                await MediaService.UpdateMediaTypeAsync(_model);
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
        private void OpenDialog()
        {
            _isEdit = false;
            _model = new();
            visible = true;
        }
        private void EditItem(MediaTypeObjVM item,bool isEdit)
        {
            _model.Id = item.Id;
            _model.NameAr = item.NameAr;
            _model.NameEn = item.NameEn;
            _model.IsActive = item.IsActive;
            _isEdit = isEdit;
            visible = true;
           
               
            
        }
        protected async override Task OnInitializedAsync()
        {
            var result = await MediaService.GetMediaTypesAsync(_pageNumber = 1, _pageSize = 10);
            _mediaTypes = result.Value.Records;
        }
    }
}
