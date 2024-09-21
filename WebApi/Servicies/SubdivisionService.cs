using AutoMapper;
using WebApi.Entity;
using WebApi.Models;
using WebApi.Repositories;

namespace WebApi.Servicies
{
    public class SubdivisionService(ISubdivisionRepository repository, IMapper mapper) : ISubdivisionService
    {
        public async Task<List<SubdivisionStatusResponse>> GetSubdivisionsStatuses()
        {
            List<Subdivision> subdivisions = await repository.GetSubdivisions();
            return mapper.Map<List<SubdivisionStatusResponse>>(subdivisions);
        }
    }
}
