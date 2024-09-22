using WebApi.Entity;

namespace WebApi.Repositories
{
    public interface ISubdivisionRepository
    {
        Task<List<Subdivision>> GetAll();

        Task<Subdivision?> GetById(int id);

        Task Add(Subdivision subdivision);

        Task Update(Subdivision subdivision);
    }
}
