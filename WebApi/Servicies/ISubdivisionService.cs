using WebApi.Models;

namespace WebApi.Servicies
{
    public interface ISubdivisionService
    {
        Task<List<SubdivisionStatusResponse>> GetSubdivisionsStatuses();
    }
}
