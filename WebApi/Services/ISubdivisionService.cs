using WebApi.Models;

namespace WebApi.Services
{
    public interface ISubdivisionService
    {
        Task<List<SubdivisionResponse>> GetAll();

        Task<SubdivisionResponse> GetById(int id);

        Task Add(SubdivisionRequest subdivision);

        Task Update(int id, SubdivisionRequest subdivision);
    }
}
