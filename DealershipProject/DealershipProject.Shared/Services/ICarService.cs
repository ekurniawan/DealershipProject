using DealershipProject.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealershipProject.Shared.Services
{
    public interface ICarService
    {
        Task<List<Car>> GetCarsAsync();
        Task<Car?> GetCarByIdAsync(int carId);
        Task<Car?> CreateCarAsync(Car car);
        Task<Car?> UpdateCarAsync(int carId, Car car);
        Task<bool> DeleteCarAsync(int carId);
    }
}
