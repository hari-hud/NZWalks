using Microsoft.AspNetCore.Mvc;
using NZWalks.Models.DTO;

namespace NZWalks.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CountriesController : ControllerBase
{
    [HttpGet]
    public ActionResult Get()
    {
        var countriesDomainModel = CountriesData.Get();

        // Map Domain Model to DTO
        var response = new List<CountryDto>();
        foreach(var countryDomain in countriesDomainModel)
        {
            response.Add(new CountryDto
            {
                Id = countryDomain.Id,
                Name = countryDomain.Name
            });
        }

        return Ok(response);
    }
}
