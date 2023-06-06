using DataAccessLayer.Data;
using DataAccessLayer.DTOs;
using DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HSPA_BACKEND.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class PropertyTypeController : ControllerBase
    {
        private readonly DataContext dbContext;
        private readonly IPropertyTypeRepository propertyTypeRepository;

        public PropertyTypeController(DataContext dbContext, IPropertyTypeRepository propertyTypeRepository)
        {
            this.dbContext = dbContext;
            this.propertyTypeRepository = propertyTypeRepository;
        }

        [HttpGet("list")]
        [AllowAnonymous]
        public async Task<IActionResult> GetFurnishingTypes()
        {
            var propertyTypes = await propertyTypeRepository.GetPropertyTypesAsync();
            var propertyTypeDtos = new List<PropertyTypeDto>();

            foreach (var propertyType in propertyTypes)
            {
                var propertyTypeDto = new PropertyTypeDto
                {
                    Id = propertyType.Id,
                    Name = propertyType.Name
                };

                propertyTypeDtos.Add(propertyTypeDto);
            }

            return Ok(propertyTypeDtos);
        }
    }
}
