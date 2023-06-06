using DataAccessLayer.Data;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IPropertyRepository
    {
        Task<Property> GetPropertyByIdAsync(Guid id);
        Task<IEnumerable<Property>> GetPropertiesAsync();
        Task<List<Property>> PropertyListingAsync();
        Task<Property> DeletePropertyByIdAsync(Guid id);
    }
}
