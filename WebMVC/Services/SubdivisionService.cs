using WebMVC.Extensions;
using WebMVC.Models;

namespace WebMVC.Services
{
    public class SubdivisionService(HttpClient client, IConfiguration configuration) : ISubdivisionService
    {
        public async Task<List<Subdivision>> GetAll()
        {
            var response = await client.GetAsync(configuration["URLs:WebAPI"]);

            return await response.ReadContentAsync<List<Subdivision>>();
        }
    }
}
