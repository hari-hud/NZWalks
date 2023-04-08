using NZWalks.Models.Domain;

namespace NZWalks.Repositories
{
    public class InMemoryRegionRepository: IRegionRepository
    {
        public async Task<List<Region>> GetAllAsync()
        {
            return new List<Region>
            {
                new Region
                {
                    Id = Guid.NewGuid(),
                    Code = "Sample Code",
                    Name = "Sample Name",
                    RegionImageUrl = "smaple url"
                }
            };
        }
    }
}
