using Microsoft.AspNetCore.Mvc;
using NZWalks.Data;
using NZWalks.Models.Domain;

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
        var regions = dbContext.Regions.ToList();
        return Ok(regions); 
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
