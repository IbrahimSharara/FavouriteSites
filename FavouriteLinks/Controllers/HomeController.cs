using FavouriteLinks.Models;
using FavouriteLinks.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FavouriteLinks.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISiteRepository repository;

        public HomeController(ILogger<HomeController> logger, ISiteRepository repository)
        {
            _logger = logger;
            this.repository = repository;
        }

        public IActionResult Index()
        {
            ViewBag.sites = repository.GetSiteViewModels(TempData.Peek("userId").ToString());
            return View();
        }
        [HttpPost]
        public IActionResult AddNewLink(SiteViewModel model)
        {
            model.UserId = TempData.Peek("userId").ToString();
            if (ModelState.IsValid)
            {
                ViewBag.sites = repository.GetSiteViewModels(TempData.Peek("userId").ToString());
                repository.Insert(model);
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            repository.Delete(id);
            return RedirectToAction("Index");
        }

            [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}