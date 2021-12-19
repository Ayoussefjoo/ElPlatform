using Blazored.LocalStorage;
using ElPlatform.Client.Services.Exceptions;
using ElPlatform.Client.Services.Interfaces;
using ElPlatform.Shared.Models;
using ElPlatform.Shared.Responses;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ElPlatform.App.Components
{
    public partial class RegisterForm
    {
        [Inject]
        public IAuthenticationService AuthenticationService { get; set; }
        [Inject]
        public NavigationManager Navigation { get; set; }
     
        private RegisterRequest _model = new();
        private bool _isBusy = false;
        private string _errorMessage = string.Empty;
        private async Task RegisterUserAsync()
        {
            _isBusy = true;
            _errorMessage = string.Empty;
            try
            {
                await AuthenticationService.RegisterUserAsync(_model);
                Navigation.NavigateTo("/auth/login");
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
        private void RedirectToLogin()
        {
            Navigation.NavigateTo("/auth/login");
        }
    }
}
