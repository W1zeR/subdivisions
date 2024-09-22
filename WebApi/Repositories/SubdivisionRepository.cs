using Microsoft.EntityFrameworkCore;
using WebApi.Context;
using WebApi.Entity;

namespace WebApi.Repositories
{
    public class SubdivisionRepository(AppDbContext context) : ISubdivisionRepository
    {
        public async Task Add(Subdivision subdivision)
        {
            await context.Subdivisions.AddAsync(subdivision);
            await context.SaveChangesAsync();
        }

        public async Task<List<Subdivision>> GetAll()
        {
            return await context.Subdivisions.OrderBy(s => s.Id).ToListAsync();
        }

        public async Task<Subdivision?> GetById(int id)
        {
            return await context.Subdivisions.FindAsync(id);
        }

        public async Task Update(Subdivision subdivision)
        {
            context.Subdivisions.Update(subdivision);
            await context.SaveChangesAsync();
        }
    }
}
