using AutoMapper;
using Newtonsoft.Json;
using System.Text;
using WebMVC.Extensions;
using WebMVC.Models;

namespace WebMVC.Services
{
    public class SubdivisionService(IHttpClientFactory httpClientFactory, IConfiguration configuration,
        IMapper mapper) : ISubdivisionService
    {
        public async Task<List<Subdivision>> GetAll()
        {
            using HttpClient client = httpClientFactory.CreateClient();

            var response = await client.GetAsync(configuration["URLs:WebAPI"]);

            return await response.ReadContentAsync<List<Subdivision>>();
        }

        public async Task<List<Subdivision>> Search(string search)
        {
            List<Subdivision> subdivisions = await GetAll();
            if (!string.IsNullOrEmpty(search))
            {
                subdivisions = subdivisions.Where(s => s.Name.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            return subdivisions;
        }

        public async Task SyncWithFile(IFormFile file)
        {
            using var stream = new StreamReader(file.OpenReadStream());
            var json = await stream.ReadToEndAsync();
            var departmentsFromFile = JsonConvert.DeserializeObject<List<Subdivision>>(json);
            await SyncAll(departmentsFromFile!);
        }

        private async Task SyncAll(List<Subdivision> subdivisionsFromFile)
        {
            List<Subdivision> subdivisionsFromDb = await GetAll();

            using HttpClient client = httpClientFactory.CreateClient();

            foreach (var fileSubdivision in subdivisionsFromFile)
            {
                await SyncOne(fileSubdivision, subdivisionsFromDb, client);
            }
        }

        private async Task SyncOne(Subdivision fileSubdivision, List<Subdivision> subdivisionsFromDb, HttpClient client)
        {
            var dbSubdivision = subdivisionsFromDb.FirstOrDefault(s => s.Id == fileSubdivision.Id);

            // Если подразделение отсутствует в БД, но есть в файле
            if (dbSubdivision == null)
            {
                await Add(fileSubdivision, client);
            }
            else
            {
                if (dbSubdivision.MainId != fileSubdivision.MainId)
                {
                    dbSubdivision.MainId = fileSubdivision.MainId;
                    await Update(dbSubdivision, client);
                }
            }
        }

        private async Task Add(Subdivision subdivision, HttpClient client)
        {
            StringContent stringContent = GetStringContent(subdivision);
            await client.PostAsync(configuration["URLs:WebAPI"], stringContent);
        }

        private async Task Update(Subdivision subdivision, HttpClient client)
        {
            StringContent stringContent = GetStringContent(subdivision);
            await client.PutAsync(configuration["URLs:WebAPI"] + $"/{subdivision.Id}", stringContent);
        }

        private StringContent GetStringContent(Subdivision subdivision)
        {
            SubdivisionRequest request = mapper.Map<SubdivisionRequest>(subdivision);
            var json = JsonConvert.SerializeObject(request);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}
