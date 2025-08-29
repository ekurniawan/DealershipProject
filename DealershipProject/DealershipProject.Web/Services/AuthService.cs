using DealershipProject.Shared.Services;

namespace DealershipProject.Web.Services
{
    public class AuthService : IAuthService
    {
        private readonly IHttpContextAccessor _httpContextAssesor;
        private string? _currentToken;
        private string? _currentEmail;
        private HttpContext? context;

        public AuthService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAssesor = httpContextAccessor;
            context = _httpContextAssesor.HttpContext;
        }

        public bool IsAuthenticated => !string.IsNullOrEmpty(_currentToken);


        public async Task ClearTokenAsync()
        {
            _currentToken = null;
            _currentEmail = null;

            if (context?.Session != null)
            {
                context.Session.Remove("auth_token");
                context.Session.Remove("auth_email");
            }
            await Task.CompletedTask;
        }

        public async Task<string?> GetEmailAsync()
        {
            if (_currentEmail != null)
                return _currentEmail;
            if (context?.Session != null)
            {
                _currentEmail = context.Session.GetString("auth_email");
            }
            return await Task.FromResult(_currentEmail);
        }

        public async Task<string?> GetTokenAsync()
        {
            if (_currentToken != null)
                return _currentToken;

            if (context?.Session != null)
            {
                _currentToken = context.Session.GetString("auth_token");
            }
            return await Task.FromResult(_currentToken);
        }

        public async Task SetEmailAsync(string email)
        {
            _currentEmail = email;
            if (context?.Session != null)
            {
                context.Session.SetString("auth_email", email);
            }
            await Task.CompletedTask;
        }

        public async Task SetTokenAsync(string token)
        {
            _currentToken = token;
            if (context?.Session != null)
            {
                context.Session.SetString("auth_token", token);
            }
            await Task.CompletedTask;
        }
    }
}
