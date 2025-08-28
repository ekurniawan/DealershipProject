using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealershipProject.Shared.Services
{
    public interface IAuthService
    {
        Task<string> GetTokenAsync();
        Task SetTokenAsync(string token);
        Task ClearTokenAsync();
        Task<string> GetEmailAsync();
        Task SetEmailAsync(string email);
        bool IsAuthenticated { get; }
    }
}
