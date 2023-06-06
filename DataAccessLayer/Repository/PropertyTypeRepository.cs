using DataAccessLayer.Data;
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
    public class PropertyTypeRepository : IPropertyTypeRepository

    {
        private readonly DataContext dbContext;

        public PropertyTypeRepository(DataContext dbContext)
        {   
            this.dbContext = dbContext;
        }

        public async Task<List<PropertyType>> GetPropertyTypesAsync()
        {
        return await dbContext.PropertyTypes.ToListAsync();
        }



    }
}
