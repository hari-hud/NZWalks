using Microsoft.AspNetCore.Mvc;
using NZWalks.Data;
using NZWalks.Models.Domain;
using NZWalks.Models.DTO;

namespace NZWalks.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RegionsController : ControllerBase
{
    private readonly NZWalkDbContext dbContext;

    public RegionsController(NZWalkDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        // Get Data from Database Model - Domain Model
        var regions = dbContext.Regions.ToList();

        // Map domain model to DTO
        // This does not expose DB layer to view
        var regionDto = new List<RegionDto>();
        foreach(var region in regions)
        {
            regionDto.Add(new RegionDto()
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl
            });
        }

        // Return DTO
        return Ok(regionDto); 
    }

    [HttpGet]
    [Route("{id:Guid}")]
    public IActionResult GetById([FromRoute] Guid id)
    {
        
        // var region = dbContext.Regions.Find(id); 
        // OR
        var region = dbContext.Regions.FirstOrDefault(x => x.Id == id);
        // FirstOrDefault can be used to check other prop however with Find we can use only id

        if (region == null) 
        {
            return NotFound();
        }

        // Map/Convert to DTO
        RegionDto regionDto = new RegionDto
        {
            Id = region.Id,
            Code = region.Code,
            Name = region.Name,
            RegionImageUrl = region.RegionImageUrl
        };

        // return DTO
        return Ok(regionDto); 
    }


    [HttpPost]
    public IActionResult Create([FromBody] AddRegionRequestDto request)
    {
        // Convert DTO to Domain Model
        Region region = new Region 
        {
            Code = request.Code,
            Name = request.Name,
            RegionImageUrl = request.RegionImageUrl
            
        };

        // Use domain model to create region
        dbContext.Regions.Add(region);
        dbContext.SaveChanges();

        // Map Domain Model to DTO
        RegionDto response = new RegionDto
        {
            Id = region.Id,
            Code = region.Code,
            Name = region.Name,
            RegionImageUrl = region.RegionImageUrl
        };

        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    [HttpPut]
    [Route("{id:Guid}")]
    public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateRegionRwquestDto request)
    {
        // chekc if region exist
        var region = dbContext.Regions.FirstOrDefault(x => x.Id == id);

        if (region == null) {
            return NotFound();
        }

        // convert DTO to domain model
        region.Code = request.Code;
        region.Name = request.Name;
        region.RegionImageUrl = request.RegionImageUrl;

        dbContext.SaveChanges();

        // Map Domain Model to DTO
        RegionDto response = new RegionDto
        {
            Id = region.Id,
            Code = region.Code,
            Name = region.Name,
            RegionImageUrl = region.RegionImageUrl
        };

        return Ok(response);
    }

    [HttpDelete]
    [Route("{id:Guid}")]
    public IActionResult Delete([FromRoute] Guid id)
    {
        var region = dbContext.Regions.FirstOrDefault(x => x.Id == id);

        if (region == null) 
        {
            return NotFound();
        }

        // delete 
        dbContext.Regions.Remove(region);
        dbContext.SaveChanges();


        // return OK (optionally we can return deleted object)
        return Ok(); 
    }
}
