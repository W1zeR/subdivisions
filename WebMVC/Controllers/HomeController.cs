using Microsoft.AspNetCore.Mvc;
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
