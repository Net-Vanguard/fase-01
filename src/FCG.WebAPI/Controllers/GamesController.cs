using Microsoft.AspNetCore.Mvc;

namespace FCG.WebAPI.Controllers
{
    public class GamesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
