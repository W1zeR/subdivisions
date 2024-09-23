using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Cors;
using WebApi.Models;
using WebApi.Servicies;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("subdivisions")]
    [EnableCors(origins: "http://localhost:37566,http://localhost:5177,https://localhost:7170,http://localhost:5177", 
        headers: "*", methods: "*")]
    public class SubdivisionController(ISubdivisionService service) : ControllerBase
    {
        [HttpGet]
        public async Task<List<SubdivisionResponse>> GetAll()
        {
            return await service.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<SubdivisionResponse> GetById([FromRoute] int id)
        {
            return await service.GetById(id);
        }

        [HttpPost]
        public async Task Add([FromBody] SubdivisionRequest subdivision)
        {
            await service.Add(subdivision);
        }

        [HttpPut("{id}")]
        public async Task Update([FromRoute] int id, [FromBody] SubdivisionRequest subdivision)
        {
            await service.Update(id, subdivision);
        }
    }
}
