using NZWalks.Models.Domain;
using NZWalks.Models.DTO;

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

        Task<Region> IRegionRepository.CreateAsync(Region region)
        {
            throw new NotImplementedException();
        }

        Task<Region?> IRegionRepository.DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        Task<Region?> IRegionRepository.GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        Task<Region?> IRegionRepository.UpdateAsync(Guid id, Region region)
        {
            throw new NotImplementedException();
        }
    }
}
