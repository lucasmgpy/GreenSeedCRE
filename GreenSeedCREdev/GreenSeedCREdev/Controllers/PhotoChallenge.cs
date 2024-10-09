using Microsoft.AspNetCore.Mvc;

namespace GreenSeedCRE.Controllers
{
    public class PhotoChallenge : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
