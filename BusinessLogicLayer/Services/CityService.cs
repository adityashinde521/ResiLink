using BusinessLogicLayer.Exceptions;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class CityService : ICityService

    {
        private readonly ICityBll cityRepository;

        public CityService(ICityBll cityRepository)
        {
            this.cityRepository = cityRepository;
        }

        public async Task<City> CreateAsync(City city)
        {
            if (city.Name.Length > 25)
                throw new BadRequestException("City Length is more than 25 characters");

            if (city.Name.Length > 25)
                throw new BadRequestException("City Length is more than 25 characters");

            var res = await cityRepository.CreateAsync(city);
            return res;

        }

        public async Task<City?> DeleteAsync(Guid Id)
        {
            var res = await cityRepository.DeleteAsync(Id);
            return res;
        }

        public async Task<List<City>> GetAllAsync()
        {
            var res = await cityRepository.GetAllAsync();
            return res; //Return awit not needed
        }

        public async Task<City?> GetByIdAsync(Guid Id)
        {
            var res = await cityRepository.GetByIdAsync(Id);
            return res;
        }

        public async Task<City?> UpdateAsync(Guid Id, City city)
        {
            var res = await cityRepository.UpdateAsync(Id, city);
            return res;

        }
    }
}
