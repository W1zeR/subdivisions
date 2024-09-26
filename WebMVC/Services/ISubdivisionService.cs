using WebMVC.Models;

namespace WebMVC.Services
{
    public interface ISubdivisionService
    {
        Task<List<Subdivision>> GetAll();

        Task<List<Subdivision>> Search(string search);

        Task SyncWithFile(IFormFile file);
    }
}
