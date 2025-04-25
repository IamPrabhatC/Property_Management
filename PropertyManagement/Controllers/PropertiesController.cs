using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PropertyManagement.Api.Models.Property;
using PropertyManagement.Application.Dtos;
using PropertyManagement.Application.Interfaces;

namespace PropertyManagement.Controllers;

/// <summary>
/// Interface for CRUD Operations for Property
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class PropertiesController : ControllerBase
{
    private readonly IPropertyService propertyService;
    private readonly IMapper mapper;

    public PropertiesController(IPropertyService propertyService, IMapper mapper)
    {
        this.propertyService = propertyService;
        this.mapper = mapper;
    }

    /// <summary>
    /// Create an Peoperty Record
    /// </summary>
    /// <param name="propertyRequest"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PropertyRequestModel propertyRequest)
    {
        
        var propertyDto = mapper.Map<PropertyRequestDTO>(propertyRequest);

        var createdPropertyDto = await propertyService.CreatePropertyAsync(propertyDto);

        // Map created DTO to response model
        var response = mapper.Map<PropertyResponseModel>(createdPropertyDto);

        return CreatedAtAction(nameof(GetById), new { id = response.ExternalId }, response);
    }

    /// <summary>
    /// Fetch Property by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var propertyDto = await propertyService.GetPropertyByIdAsync(id);

        if (propertyDto == null)
        {
            return new NotFoundObjectResult($"Property with ID {id} was not found");
        }

        var response = mapper.Map<PropertyResponseModel>(propertyDto);
        return Ok(response);
    }

    /// <summary>
    /// Fetch All Properties
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {        
        var propertiesDto = await propertyService.GetAllPropertiesAsync();

      
        var responses = mapper.Map<IEnumerable<PropertyResponseModel>>(propertiesDto);

        return Ok(responses);
    }

    /// <summary>
    /// Update Property
    /// </summary>
    /// <param name="id"></param>
    /// <param name="propertyRequest"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] PropertyRequestModel propertyRequest)
    {                
        var propertyDto = mapper.Map<PropertyRequestDTO>(propertyRequest);

        // Call service to update the property
        var updatedPropertyDto = await propertyService.UpdatePropertyAsync(id, propertyDto);  
      
        var response = mapper.Map<PropertyResponseModel>(updatedPropertyDto);

        return Ok(response);
    }

    /// <summary>
    /// Delete Property by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await propertyService.DeletePropertyAsync(id);     

        return NoContent();
    }
}
