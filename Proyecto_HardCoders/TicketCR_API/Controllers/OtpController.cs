using AppLogic;
using DTO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TicketCR_API.Controllers
{
	[EnableCors("MyCorsPolicy")]
	[ApiController]
	[Route("api/[controller]/[action]")]
	public class OtpController : ControllerBase
    {
		[EnableCors("MyCorsPolicy")]
		[HttpPost]
        public string CreateOtp(Otp otp)
        {
            OtpManager om = new OtpManager();

            om.CreateOtp(otp);

            return "ok";
        }
    }
}
