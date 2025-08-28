using DealershipProject.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealershipProject.Shared.Services
{
    public interface ILoginService
    {
        Task<AuthResponse> LoginAsync(LoginRequest loginRequest);
    }
}
