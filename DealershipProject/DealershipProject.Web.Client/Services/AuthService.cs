using DealershipProject.Shared.Services;
using Microsoft.JSInterop;

namespace DealershipProject.Web.Client.Services
{
    public class AuthService : IAuthService
    {
        private readonly IJSRuntime _jsRuntime;
        private string? _currentToken;
        private string? _currentEmail;

        public AuthService(IJSRuntime jSRuntime)
        {
            _jsRuntime = jSRuntime;
        }

        public bool IsAuthenticated => !string.IsNullOrEmpty(_currentToken);


        public async Task ClearTokenAsync()
        {
            try
            {
                _currentToken = null;
                await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "auth_token");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error clearing token from localStorage: {ex.Message}");
            }
        }

        public async Task<string?> GetEmailAsync()
        {
            try
            {
                _currentEmail = await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", "auth_email");
                return _currentEmail;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting email from localStorage: {ex.Message}");
                return null;
            }
        }

        public async Task<string?> GetTokenAsync()
        {
            try
            {
                _currentToken = await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", "auth_token");
                return _currentToken;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting token from localStorage: {ex.Message}");
                return null;
            }
        }

        public async Task SetEmailAsync(string email)
        {
            _currentEmail = email;
            try
            {
                await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "auth_email", email);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error setting email in localStorage: {ex.Message}");
            }
        }

        public async Task SetTokenAsync(string token)
        {
            _currentToken = token;
            try
            {
                await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "auth_token", token);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error setting token in localStorage: {ex.Message}");
            }
        }
    }
}
