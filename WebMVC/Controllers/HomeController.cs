using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebMVC.Exceptions;
using WebMVC.Models;
using WebMVC.Services;

namespace WebMVC.Controllers
{
    public class HomeController(ISubdivisionService service, ILogger<HomeController> logger) : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<Subdivision> subdivisions = await service.GetAll();
            return View(subdivisions);
        }

        public async Task<IActionResult> Subdivisions(string search)
        {
            List<Subdivision> subdivisions = await service.Search(search);
            return PartialView(subdivisions);
        }

        public async Task<IActionResult> Sync(IFormFile file)
        {
            try
            {
                ValidateFileNotEmpty(file);
                ValidateFileIsJson(file);
            }
            catch (FormFileException e)
            {
                TempData["SyncResult"] = e.Message;
                return RedirectToAction("Index");
            }

            try
            {
                await service.SyncWithFile(file);
                TempData["SyncResult"] = "Синхронизация данных прошла успешно";
            }
            catch (Exception)
            {
                logger.LogError("Error in method Sync of HomeController");
                TempData["SyncResult"] = "Произошла ошибка синхронизации данных";
            }

            return RedirectToAction("Index");
        }

        private static void ValidateFileNotEmpty(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new FormFileException("Файл не выбран или он пустой");
            }
        }

        private static void ValidateFileIsJson(IFormFile file)
        {
            if (file.ContentType != "application/json")
            {
                throw new FormFileException("Необходим файл с расширением json");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
