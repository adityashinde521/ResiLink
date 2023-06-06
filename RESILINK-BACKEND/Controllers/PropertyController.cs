using AutoMapper;
using DataAccessLayer.Data;
using DataAccessLayer.DTOs;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HSPA_BACKEND.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertyListingController : ControllerBase
    {
        private readonly DataContext dbContext;
        private readonly IPropertyRepository _propertyRepository;
        private readonly IMapper _mapper;

        public PropertyListingController(DataContext dbContext, IPropertyRepository propertyRepository, IMapper mapper)
        {
            this.dbContext = dbContext;
            _propertyRepository = propertyRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetProperties()
        {
            var properties = await _propertyRepository.GetPropertiesAsync();
            var propertyDtoList = _mapper.Map<IEnumerable<PropertyListingDto>>(properties);
            return Ok(propertyDtoList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProperty(Guid id)
        {
            var property = await _propertyRepository.GetPropertyByIdAsync(id);
            if (property == null)
                return NotFound();

            var propertyDto = _mapper.Map<PropertyListingDto>(property);
            return Ok(propertyDto);
        }
        [Authorize(Roles="PropertyManager")]
        [HttpPost]
        public async Task<IActionResult> CreateProperty(PropertyListingDto propertyDto)
        {
            var property = _mapper.Map<Property>(propertyDto);
            
            dbContext.Properties.Add(property);
             await dbContext.SaveChangesAsync();

            // Optionally, you can return the created entity or any other response
            return CreatedAtAction(nameof(GetProperty), new { id = property.Id }, property);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProperty(Guid id)
        {
            var property = await _propertyRepository.DeletePropertyByIdAsync(id);
            if (property == null)
                return NotFound();

            return Ok(property);
        }
    }
}
