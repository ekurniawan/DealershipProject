using DealershipProject.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealershipProject.Services
{
    public class AuthService : IAuthService
    {
        private string? _currentToken;
        private string? _currentEmail;

        public bool IsAuthenticated => !string.IsNullOrEmpty(_currentToken);

        public async Task<string?> GetTokenAsync()
        {
            if (_currentToken != null)
                return _currentToken;

            // Try to get token from secure storage
            try
            {
                _currentToken = await SecureStorage.GetAsync("auth_token");
                return _currentToken;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting token from secure storage: {ex.Message}");
                return null;
            }
        }

        public async Task SetTokenAsync(string token)
        {
            _currentToken = token;

            // Store in secure storage
            try
            {
                await SecureStorage.SetAsync("auth_token", token);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error saving token to secure storage: {ex.Message}");
            }
        }

        public async Task<string?> GetEmailAsync()
        {
            if (_currentEmail != null)
                return _currentEmail;

            // Try to get email from secure storage
            try
            {
                _currentEmail = await SecureStorage.GetAsync("auth_email");
                return _currentEmail;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting email from secure storage: {ex.Message}");
                return null;
            }
        }

        public async Task SetEmailAsync(string email)
        {
            _currentEmail = email;

            // Store in secure storage
            try
            {
                await SecureStorage.SetAsync("auth_email", email);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error saving email to secure storage: {ex.Message}");
            }
        }

        public async Task ClearTokenAsync()
        {
            _currentToken = null;
            _currentEmail = null;

            try
            {
                SecureStorage.Remove("auth_token");
                SecureStorage.Remove("auth_email");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error clearing data from secure storage: {ex.Message}");
            }

            await Task.CompletedTask;
        }
    }
}
