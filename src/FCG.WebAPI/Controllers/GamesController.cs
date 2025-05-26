using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FCG.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class GamesController : Controller
    {
        [Authorize(Roles = "Administrator")]

        public IActionResult Index()
        {
            return View();
        }
    }
}
