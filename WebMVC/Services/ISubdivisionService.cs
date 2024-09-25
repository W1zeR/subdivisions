using WebMVC.Models;

namespace WebMVC.Services
{
    public interface ISubdivisionService
    {
        Task<List<Subdivision>> GetAll();

        Task Sync(List<Subdivision> subdivisionsFromFile);
    }
}
