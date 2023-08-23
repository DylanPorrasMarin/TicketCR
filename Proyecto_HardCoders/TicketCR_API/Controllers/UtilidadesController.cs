using AppLogic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace TicketCR_API.Controllers
{
    [EnableCors("MyCorsPolicy")]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UtilidadesController : ControllerBase
    {


        [HttpGet]
        public byte[] GetQR(string content)
        {
            AdminQR adminQR = new AdminQR();
            return adminQR.GenerateQR(content);
        }
    }
}
