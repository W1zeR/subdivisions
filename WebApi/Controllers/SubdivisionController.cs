using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Servicies;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("subdivisions")]
    public class SubdivisionController(ISubdivisionService service) : ControllerBase
    {
        [HttpGet("statuses")]
        public async Task<List<SubdivisionStatusResponse>> GetSubdivisionsStatuses()
        {
            return await service.GetSubdivisionsStatuses();
        }
    }
}
