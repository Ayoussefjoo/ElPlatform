﻿using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ElPlatform.App
{
    public class JWTAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _storage;
        public JWTAuthenticationStateProvider(ILocalStorageService storage)
        {
            _storage = storage;
        }
        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            if (await _storage.ContainKeyAsync("access_token"))
            {
                //The user is logged in 
                var tokenAsString =await _storage.GetItemAsStringAsync("access_token");
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.ReadJwtToken(tokenAsString);
                var identity = new ClaimsIdentity(token.Claims,"Bearer");
                var user = new ClaimsPrincipal(identity);
                var authState = new AuthenticationState(user);
                NotifyAuthenticationStateChanged(Task.FromResult(authState));
                return authState;
            }
            return new AuthenticationState(new ClaimsPrincipal()); // Empty claims means no identity and user not loggedin 
        }
    }
}
