using Microsoft.AspNetCore.Mvc;
using NZWalks.Models.DTO;

namespace NZWalks.V1.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CountriesController : ControllerBase
{
    [HttpGet]
    public ActionResult Get()
    {
        var countriesDomainModel = CountriesData.Get();

        // Map Domain Model to DTO
        var response = new List<CountryDtoV1>();
        foreach(var countryDomain in countriesDomainModel)
        {
            response.Add(new CountryDtoV1
            {
                Id = countryDomain.Id,
                Name = countryDomain.Name
            });
        }

        return Ok(response);
    }
}
