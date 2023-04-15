using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.Data;
using NZWalks.Models.Domain;
using NZWalks.Models.DTO;
using NZWalks.Repositories;

namespace NZWalks.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WalkController : ControllerBase
{
    private readonly IWalkRepository walkRepository;
    private readonly IMapper mapper;

    public WalkController(NZWalkDbContext dbContext, IWalkRepository walkRepository, IMapper mapper)
    {
        this.walkRepository = walkRepository;
        this.mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        // Get Data from Database Model - Domain Model
         var walks = await walkRepository.GetAllAsync();

        // Map domain model to DTO
        var walkDto = mapper.Map<List<WalkDto>>(walks);
        
        // Return DTO
        return Ok(walkDto); 
    }

    [HttpGet]
    [Route("{id:Guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    { 
        var walk = await walkRepository.GetByIdAsync(id);
        if (walk == null) 
        {
            return NotFound();
        }

        // Map domain model to DTO & return DTO
        return Ok(mapper.Map<WalkDto>(walk)); 
    }


    [HttpPost]
    [ValidateModel]
    public async Task<IActionResult> Create([FromBody] CreateWalkDto request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        // Convert DTO to Domain Model
        var walk = mapper.Map<Walk>(request);

        // Use domain model to create region
        walk = await walkRepository.CreateAsync(walk);

        // Map Domain Model to DTO
        WalkDto response  = mapper.Map<WalkDto>(walk);

        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    [HttpPut]
    [Route("{id:Guid}")]
    [ValidateModel]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWalkDto request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        // Convert DTO to Domain Model
        var walk = mapper.Map<Walk>(request);

        walk = await walkRepository.UpdateAsync(id, walk);

        if (walk == null) {
            return NotFound();
        }

        // Map Domain Model to DTO
        WalkDto response  = mapper.Map<WalkDto>(walk);

        return Ok(response);
    }

    [HttpDelete]
    [Route("{id:Guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var walk = await walkRepository.DeleteAsync(id);

        if (walk == null) 
        {
            return NotFound();
        }

        return Ok(); 
    }
}
