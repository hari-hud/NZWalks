using NZWalks.Data;
using NZWalks.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace NZWalks.Repositories
{
    public class MySqlWalkRepository: IWalkRepository
    {
        private readonly NZWalkDbContext dbContext;

        public MySqlWalkRepository(NZWalkDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Walk>> GetAllAsync()
        {
            return await dbContext.Walks
                .Include("Difficulty")
                .Include("Region")
                .ToListAsync();
        }

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            return await dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk> CreateAsync(Walk walk)
        {
            await dbContext.Walks.AddAsync(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
        {
            var existingWalk = await GetByIdAsync(id);

            if (existingWalk == null) {
                return null;
            }

            existingWalk.Name = walk.Name;
            existingWalk.Description = walk.Description;
            existingWalk.LengthInKm = walk.LengthInKm;
            existingWalk.WalkImageUrl = walk.WalkImageUrl;
            existingWalk.DifficultyId = walk.DifficultyId;
            existingWalk.LengthInKm = walk.LengthInKm;

            await dbContext.SaveChangesAsync();
            return existingWalk;
        }

        public async Task<Walk?> DeleteAsync(Guid id)
        {
            var existingWalk = await GetByIdAsync(id);

            if (existingWalk == null) {
                return null;
            }

            dbContext.Walks.Remove(existingWalk); // remove does not have Async
            await dbContext.SaveChangesAsync();
            return existingWalk;
        }
    }
}
