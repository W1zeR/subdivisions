using WebApi.Entity;

namespace WebApi.Repositories
{
    public interface ISubdivisionRepository
    {
        Task<List<Subdivision>> GetSubdivisions();
    }
}
