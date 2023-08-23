using Microsoft.AspNetCore.Mvc;

namespace TicketCR_UI.Controllers
{
    public class HardcodersLandingController : Controller
    {
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult LandingHardcoders()
        {
            return View();
        }
     

    }
    
}
