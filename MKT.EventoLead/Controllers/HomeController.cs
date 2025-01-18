using Microsoft.AspNetCore.Mvc;

namespace MKT.EventoLead.WebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return NotFound();
        }
    }
}
