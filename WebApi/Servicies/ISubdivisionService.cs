using WebApi.Models;

namespace WebApi.Servicies
{
    public interface ISubdivisionService
    {
        Task<List<SubdivisionResponse>> GetAll();

        Task<SubdivisionResponse> GetById(int id);

        Task Add(SubdivisionRequest subdivision);

        Task Update(int id, SubdivisionRequest subdivision);
    }
}
