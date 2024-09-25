using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using WebMVC.Models;
using WebMVC.Services;

namespace WebMVC.Controllers
{
    public class HomeController(ISubdivisionService service) : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<Subdivision> subdivisions = await service.GetAll();
            return View(subdivisions);
        }

        public async Task<IActionResult> Subdivisions(string search)
        {
            List<Subdivision> subdivisions = await service.GetAll();
            if (!string.IsNullOrEmpty(search))
            {
                subdivisions = subdivisions.Where(s => s.Name.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            return PartialView(subdivisions);
        }

        public async Task<IActionResult> Sync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                TempData["SyncResult"] = "Файл не выбран или он пустой";
                return RedirectToAction("Index");
            }

            if (file.ContentType != "application/json")
            {
                TempData["SyncResult"] = "Необходим файл с расширением json";
                return RedirectToAction("Index");
            }

            using (var stream = new StreamReader(file.OpenReadStream()))
            {
                var json = await stream.ReadToEndAsync();
                var departmentsFromFile = JsonConvert.DeserializeObject<List<Subdivision>>(json);

                await service.Sync(departmentsFromFile!);
            }

            TempData["SyncResult"] = "Синхронизация данных прошла успешно";
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
