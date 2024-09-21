using Microsoft.EntityFrameworkCore;
using WebApi.Context;
using WebApi.Entity;

namespace WebApi.Repositories
{
    public class SubdivisionRepository(AppDbContext context) : ISubdivisionRepository
    {
        public async Task<List<Subdivision>> GetSubdivisions()
        {
            return await context.Subdivisions.OrderBy(s => s.Id).ToListAsync();
        }
    }
}
