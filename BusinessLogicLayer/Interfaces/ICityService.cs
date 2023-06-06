using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface ICityService
    {
        Task<List<City>> GetAllAsync();
        Task<City?> GetByIdAsync(Guid Id);

        Task<City> CreateAsync(City city);

        Task<City?> UpdateAsync(Guid Id, City city);

        Task<City?> DeleteAsync(Guid Id);
    }
}
