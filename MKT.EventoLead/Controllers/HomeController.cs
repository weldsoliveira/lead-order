using Microsoft.AspNetCore.Mvc;

namespace MKT.EventoLead.WebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Sucesso = TempData["Sucesso"];
            return View();
        }
    }
}
