using Azure.Communication.Email;
using DataAccess.Crud;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace AppLogic
{
    public class AdminSMS
	{
		//string URL_DESARROLLO = "https://localhost:7176";
		string URL_PRODUCCION = "https://ticketcr-api.azurewebsites.net";
		public void SendConfirmationMessage(string emailAddress, string phone, int idUsuario)
        {
			OtpCrudFactory otpCrud = new OtpCrudFactory();
			
			Otp otp = otpCrud.ObtenerOTPporIDUsuario(idUsuario);

			int token = otp.otp;

			string url = GenerateConfirmationURL(emailAddress, idUsuario, token);
			SendSMS(phone, url);
		}

		private string GenerateConfirmationURL(string emailAddress,int idUsuario, int token)
		{
			// Aquí debes generar el URL con el idUsuario y token

			string activacionUrl = $"{URL_PRODUCCION}/TicketCR/ValidacionCuenta?email="+emailAddress+"&token="+token+"&idUsuario="+idUsuario;

			return activacionUrl;
		}


		private void SendSMS(string phone, string url)
        {
            // Find your Account SID and Auth Token at twilio.com/console
            // and set the environment variables. See http://twil.io/secure
            string accountSid = "AC4a7596569f74f70c1d91c5d8ed0f3614"; 
            string authToken = "6a29769325a13b82eb0cba32b0c57fd5";
            
            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
				
				body: "Este es el link para activar tu cuenta: " +" " + url,
                from: new Twilio.Types.PhoneNumber("+14705161220"),
                to: new Twilio.Types.PhoneNumber(phone)
            );
            Console.WriteLine(message.Sid);
        }
	}
}
