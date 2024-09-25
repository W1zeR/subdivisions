using AutoMapper;
using Newtonsoft.Json;
using System.Text;
using WebMVC.Extensions;
using WebMVC.Models;

namespace WebMVC.Services
{
    public class SubdivisionService(HttpClient client, IConfiguration configuration,
        IMapper mapper) : ISubdivisionService
    {
        public async Task<List<Subdivision>> GetAll()
        {
            var response = await client.GetAsync(configuration["URLs:WebAPI"]);

            return await response.ReadContentAsync<List<Subdivision>>();
        }

        public async Task Sync(List<Subdivision> subdivisionsFromFile)
        {
            List<Subdivision> subdivisionsFromDb = await GetAll();

            foreach (var fileSubdivision in subdivisionsFromFile)
            {
                var dbSubdivision = subdivisionsFromDb.FirstOrDefault(s => s.Id == fileSubdivision.Id);

                // Если подразделение отсутствует в БД, но есть в файле
                if (dbSubdivision == null)
                {
                    await Add(fileSubdivision);
                }
                else
                {
                    if (dbSubdivision.MainId != fileSubdivision.MainId)
                    {
                        dbSubdivision.MainId = fileSubdivision.MainId;
                        await Update(dbSubdivision);
                    }
                }
            }
        }

        private async Task Add(Subdivision subdivision)
        {
            SubdivisionRequest subdivisionRequest = mapper.Map<SubdivisionRequest>(subdivision);
            StringContent stringContent = GetStringContent(subdivisionRequest);
            await client.PostAsync(configuration["URLs:WebAPI"], stringContent);
        }

        private async Task Update(Subdivision subdivision)
        {
            SubdivisionRequest subdivisionRequest = mapper.Map<SubdivisionRequest>(subdivision);
            StringContent stringContent = GetStringContent(subdivisionRequest);
            await client.PutAsync(configuration["URLs:WebAPI"] + $"/{subdivision.Id}", stringContent);
        }

        private static StringContent GetStringContent(SubdivisionRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}
