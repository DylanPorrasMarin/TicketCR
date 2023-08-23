using AppLogic;
using Azure.Communication.Email;
using DataAccess.Crud;
using DTO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace TicketCR_API.Controllers
{
    [EnableCors("MyCorsPolicy")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CommunicationsController : ControllerBase
    {
        private readonly AdminEmail adminEmail = new AdminEmail();

        [EnableCors("MyCorsPolicy")]
        [HttpPost]
        public async Task<Api_Response> SendEmailForgotPassword(string emailAddress)
        {
            await adminEmail.SendEmailForgotPassword(emailAddress);
            return new Api_Response { Result = "Correo de recuperación enviado con éxito" };
        }

		[HttpPost]
		public async Task<Api_Response> SendEmailActivation(string emailAddress, int idUsuario)
		{
			await adminEmail.SendEmailActivation(emailAddress, idUsuario);
			return new Api_Response { Result = "Correo de activación enviado con éxito" };
		}

        [HttpPost]
        public async Task<Api_Response> SendSMS(string emailAddress, int telefono, int idUsuario)
        {
            string phone = "+506"+telefono.ToString();
            AdminSMS ae = new AdminSMS();
            ae.SendConfirmationMessage(emailAddress, phone, idUsuario);
            return new Api_Response { Result = "OK" };
        }

        [HttpPost]
        public async Task<Api_Response> CorreoMembresiaAdquirida(string emailAddress,string NombreMembresia, int IdNuevaMembresia, float CostoTotal, float Impuestos, int idUsuario, string IdPagoPaypal)
        {
            await adminEmail.CorreoMembresiaAdquirida(emailAddress, NombreMembresia, IdNuevaMembresia, CostoTotal, Impuestos, idUsuario, IdPagoPaypal);
            return new Api_Response { Result = "Correo de membresia adquirida enviado con exito" };
        }

        [HttpPost]
        public async Task<Api_Response> CorreosCompraBoletosVirtuales(List<CompraBoletosVirtuales> compraBoletos)
        {
            await adminEmail.CorreosCompraBoletosVirtuales(compraBoletos);
            return new Api_Response { Result = "Correos de compraBoletos enviados con exito" };
        }

        [HttpPost]
        public async Task<Api_Response> CorreosCompraBoletosPresenciales(List<CompraBoletosPresenciales> compraBoletos)
        {
            await adminEmail.CorreosCompraBoletosPresenciales(compraBoletos);
            return new Api_Response { Result = "Correos de compraBoletos enviados con exito" };
        }

        [HttpPost]
        public async Task<Api_Response> CorreoFacturaBoletos(List<CompraBoletosPresenciales> boletosComprados, string cuentaPaypal)
        {
            await adminEmail.CorreoFacturaBoletos(boletosComprados,cuentaPaypal);
            return new Api_Response { Result = "FACTURA BOLETOS enviados con exito" };
        }
    }
}
