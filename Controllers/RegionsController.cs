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

        if (region == null) 
        {
            return NotFound();
        }
        return Ok(region); 
    }


    [HttpPost]
    public IActionResult Create(Region region)
    {
        dbContext.Regions.Add(region);
        dbContext.SaveChanges();
        return Ok();
    }
}
