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
    public class FurnishingTypeRepository : IFurnishingTypeRepository

    {
        private readonly DataContext dbContext;

        public FurnishingTypeRepository(DataContext dbContext)
        {   
            this.dbContext = dbContext;
        }

        public async Task<List<FurnishingType>> GetFurnishingTypesAsync()
        {
        return await dbContext.FurnishingTypes.ToListAsync();
        }



    }
}
