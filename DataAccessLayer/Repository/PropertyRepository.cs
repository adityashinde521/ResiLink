using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly List<Property> _properties; // Replace this with your actual data access mechanism (e.g., database context)

        public PropertyRepository()
        {
            _properties = new List<Property>(); // Initialize with sample data or retrieve from the database
        }

        public async Task<Property> GetPropertyByIdAsync(Guid id)
        {
            return await Task.FromResult(_properties.FirstOrDefault(p => p.Id == id));
        }

        public async Task<IEnumerable<Property>> GetPropertiesAsync()
        {
            return await Task.FromResult(_properties);
            //return await dbContext.FurnishingTypes.ToListAsync();

        }

        public async Task<List<Property>> PropertyListingAsync()
        {
            return await Task.FromResult(_properties);
        }

        public async Task<Property> DeletePropertyByIdAsync(Guid id)
        {
            var property = _properties.FirstOrDefault(p => p.Id == id);
            if (property != null)
                _properties.Remove(property);

            return await Task.FromResult(property);

        }
    }
}
