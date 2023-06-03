using Microsoft.AspNetCore.Mvc;
using NZWalks.Models.DTO;

namespace NZWalks.V2.Controllers;

[ApiController]
[Route("api/v2/[controller]")]
public class CountriesController : ControllerBase
{
    [HttpGet]
    public ActionResult Get()
    {
        var countriesDomainModel = CountriesData.Get();

        // Map Domain Model to DTO
        var response = new List<CountryDtoV2>();
        foreach(var countryDomain in countriesDomainModel)
        {
            response.Add(new CountryDtoV2
            {
                Id = countryDomain.Id,
                CountryName = countryDomain.Name
            });
        }

        return Ok(response);
    }
}
