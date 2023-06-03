using NZWalks.Models.Domain;

namespace NZWalks;

public static class CountriesData
{
    public static List<Country> Get()
    {
        var countries = new[]
        {
            new {Id = 1, Name = "India"},
            new {Id = 2, Name = "United States"},
            new {Id = 3, Name = "China"},
            new {Id = 4, Name = "Russia"},
            new {Id = 5, Name = "Brazil"},
            new {Id = 6, Name = "Germany"},
            new {Id = 7, Name = "United Kingdom"},
            new {Id = 8, Name = "France"},
            new {Id = 9, Name = "Japan"},
            new {Id = 10, Name = "Australia"}
        };
        
        return countries.Select(c => new Country{ Id = c.Id, Name = c.Name}).ToList();
    }

}
