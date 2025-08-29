using DealershipProject.Shared.Services;

namespace DealershipProject.Web.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserState _userState;

        public AuthService(UserState userState)
        {
            _userState = userState;
        }

        public bool IsAuthenticated => !string.IsNullOrEmpty(_userState.Token);

        public Task<string?> GetTokenAsync()
        {
            return Task.FromResult(_userState.Token);
        }

        public Task SetTokenAsync(string token)
        {
            _userState.Token = token;
            return Task.CompletedTask;
        }

        public Task<string?> GetEmailAsync()
        {
            return Task.FromResult(_userState.Email);
        }

        public Task SetEmailAsync(string email)
        {
            _userState.Email = email;
            return Task.CompletedTask;
        }

        public Task ClearTokenAsync()
        {
            _userState.Token = null;
            _userState.Email = null;
            return Task.CompletedTask;
        }
    }
}
