using Microsoft.AspNetCore.Mvc;

namespace SignalIR.Controllers
{
    public class FileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
