using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NZWalks.Models.Domain;

namespace NZWalks.Data
{
    public class NZWalkAuthDbContext: IdentityDbContext
    {
        public NZWalkAuthDbContext(DbContextOptions options) : base(options)
        {
            
        }
    }
}
