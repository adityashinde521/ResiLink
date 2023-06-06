using AutoMapper;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.DTOs;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RESILINK_BACKEND.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class CityController : ControllerBase
    {
        private readonly ICityService cityService;

        public CityController(ICityService cityService) 
        {
            this.cityService = cityService;
        }

        //GET ALL Cities
        //https://localhost:portno/api/City
        [HttpGet]
        [Authorize(Roles = "Tenant")]
        public async Task<IActionResult> GetAll()
        {
            //Get Data from Database - Domain Model file
            //var citiesDomain = await dbContext.Cities.ToListAsync();  // No need of this already called in Repository pttern
            var citiesDomain = await cityService.GetAllAsync();

            //Map Domain Models to Dtos
            var citiesDtos = new List<CityDto>();  //comes from DTO folder
            foreach (var cityDomain in citiesDomain)
            {
                citiesDtos.Add(new CityDto()
                {
                    Id = cityDomain.Id,
                    Name = cityDomain.Name,
                    Country = cityDomain.Country,
                });

            }

            
            //Return Dtos
            return Ok(citiesDtos); //citiesDomain
        }


        //GET Single CITY (by ID)
        //https://localhost:port/api/City/id
        [HttpGet]
        [Route("{Id:Guid}")]
        [AllowAnonymous]

        public async Task<IActionResult> GetById([FromRoute] Guid Id)
        {
            //Get city domain moel from DB
            //var city = dbContext.Cities.Find(Id);
            //M-2 : 
            //var cityDomain = await dbContext.Cities.FirstOrDefaultAsync(x => x.Id == Id);
            var cityDomain = await cityService.GetByIdAsync(Id);
            if (cityDomain == null)
            {
                return NotFound();
            }

            //Mapping/Convert City Domain Model to City DTOs
            var cityDto = new CityDto
            {
                Id = cityDomain.Id,
                Name = cityDomain.Name,
                Country = cityDomain.Country,
            };

            //Return Dtos
            return Ok(cityDto);
        }


        //Post to create new City
        //https://localhost:port/api/City/
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CityChangesRequestDto addCityRequestDto)
        {
            // Reverse >> Map DTO to domain model
            var cityDomainModel = new City
            {
                Name = addCityRequestDto.Name,
                Country = addCityRequestDto.Country
            };

            // Save the city to the database or perform any necessary operations
            cityDomainModel = await cityService.CreateAsync(cityDomainModel);

            //Remapping domain model to Dtos

            var CityDtoRemapped = new CityDto
            {
                Id = cityDomainModel.Id,
                Name = cityDomainModel.Name,
                Country = cityDomainModel.Country
            };

            // Return a successful response with the created city
            return CreatedAtAction(nameof(GetById), new { Id = cityDomainModel.Id }, CityDtoRemapped);
        }


        //  Update an existing city in the Cities table.
        //PUT : https://localhost:port/api/city/{id}
        [HttpPut]
        [Authorize(Roles = "PropertyManager")]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> Update(Guid Id, [FromBody] CityChangesRequestDto updateCityRequestDto)
        {

            var cityDomainModel = new City
            {
                Name = updateCityRequestDto.Name,
                Country = updateCityRequestDto.Country
            };

            if (Id != cityDomainModel.Id)                           //Url Id Manipulation
                return BadRequest("Update Not Allowed");

            cityDomainModel = await cityService.UpdateAsync(Id, cityDomainModel);

            if (cityDomainModel == null)
            {
                return NotFound();
            }


            //Convert Domain Model to DTo
            var cityDto = new CityDto
            {
                Id = cityDomainModel.Id,
                Name = cityDomainModel.Name,
                Country = cityDomainModel.Country
            };

            return Ok(cityDto);
        }


        //Delete - Delete a city from the Cities table.
        //abc
        [HttpDelete]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid Id)
        {
            var cityDomainModel = await cityService.DeleteAsync(Id);

            if (cityDomainModel == null)
            {
                return NotFound();
            }

            //return deleted City back
            //map Domain Model to Dto

            var cityDto = new CityDto
            {
                Id = cityDomainModel.Id,
                Name = cityDomainModel.Name,
                Country = cityDomainModel.Country
            };
            return Ok(cityDto);
        }

    }
}
