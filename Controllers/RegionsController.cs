using System.Text.Json;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.Data;
using NZWalks.Models.Domain;
using NZWalks.Models.DTO;
using NZWalks.Repositories;

namespace NZWalks.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RegionsController : ControllerBase
{
    private readonly NZWalkDbContext dbContext;
    private readonly IRegionRepository regionRepository;
    private readonly IMapper mapper;
    ILogger<RegionsController> logger;

    public RegionsController(NZWalkDbContext dbContext,
                             IRegionRepository regionRepository,
                             IMapper mapper,
                             ILogger<RegionsController> logger)
    {
        this.dbContext = dbContext;
        this.regionRepository = regionRepository;
        this.mapper = mapper;
        this.logger = logger;
    }

    [HttpGet]
    [Authorize(Roles = "Reader,Writer")]
    public async Task<IActionResult> GetAll()
    {
        logger.LogInformation("Invoked GetAll() Region controller method");

        try {
            // Get Data from Database Model - Domain Model
            var regions = await regionRepository.GetAllAsync();

            // Map domain model to DTO
            var regionDto = mapper.Map<List<RegionDto>>(regions);

            logger.LogInformation($"Finished getAll regions with data: {JsonSerializer.Serialize(regionDto)}");
            
            // Return DTO
            return Ok(regionDto); 
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            throw;
        }
    }

    [HttpGet]
    [Route("{id:Guid}")]
    [Authorize(Roles = "Reader,Writer")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    { 
        var region = await regionRepository.GetByIdAsync(id);
        if (region == null) 
        {
            return NotFound();
        }

        // Map domain model to DTO & return DTO
        return Ok(mapper.Map<RegionDto>(region)); 
    }


    [HttpPost]
    [Authorize(Roles = "Writer")]
    public async Task<IActionResult> Create([FromBody] AddRegionRequestDto request)
    {
        if (ModelState.IsValid) {
            // Convert DTO to Domain Model
            var region = mapper.Map<Region>(request);

            // Use domain model to create region
            region = await regionRepository.CreateAsync(region);

            // Map Domain Model to DTO
            RegionDto response  = mapper.Map<RegionDto>(region);

            return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
        } 
        else 
        {
            return BadRequest(ModelState);
        }
        
    }

    [HttpPut]
    [Route("{id:Guid}")]
    [Authorize(Roles = "Writer")]
    [ValidateModel] // This is the alternative & clean way for if condition added in the above create method
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto request)
    {
        // Convert DTO to Domain Model
        var region = mapper.Map<Region>(request);

        region = await regionRepository.UpdateAsync(id, region);

        if (region == null) {
            return NotFound();
        }

        // Map Domain Model to DTO
        RegionDto response  = mapper.Map<RegionDto>(region);

        return Ok(response);
    }

    [HttpDelete]
    [Route("{id:Guid}")]
    [Authorize(Roles = "Writer")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var region = await regionRepository.DeleteAsync(id);

        if (region == null) 
        {
            return NotFound();
        }

        return Ok(); 
    }
}
