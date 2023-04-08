using NZWalks.Data;
using NZWalks.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace NZWalks.Repositories
{
    public class MySqlRegionRepository: IRegionRepository
    {
        private readonly NZWalkDbContext dbContext;
        public MySqlRegionRepository(NZWalkDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await dbContext.Regions.ToListAsync();
        }
    }
}
