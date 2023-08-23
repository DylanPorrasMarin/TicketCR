using Azure;
using Azure.Communication.Email;
using DataAccess.Crud;
using DTO;
using System;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Web;
using System.Net.Mail;
using Azure.Storage.Blobs;

namespace AppLogic
{


    public class AdminEmail
    {
          string URL_DESARROLLO = "https://localhost:7176";
        //string URL_PRODUCCION = "https://ticketcr.azurewebsites.net";


        #region METODO PARA ENVIAR CORREO DE RECUPERACION DE CLAVE DEL USUARIO
        public async Task<string> SendEmailForgotPassword(string emailAddress)
        {
            if (!IsValidUser(emailAddress))
            {
                // Si el correo del usuario no es válido, se envia una excepción un mensaje de error de que el correo no es valido
                throw new ArgumentException("El correo del usuario no es válido.");
            }

            string connectionString = "endpoint=https://ticketcr-communicationservice.unitedstates.communication.azure.com/;accesskey=xb5/aGM6Swnd/BYM6F+MRAL4Hk03ByUo4fYzQ+lhU0BltXIxrsy8VRutCCMWPEOzJv54ukEbmBA6fz6j7TleaA==";
            EmailClient emailClient = new EmailClient(connectionString);

            // Generar el código OTP
            string recoveryToken = EncryptionHelper.GenerateRecoveryToken();

            // URL del enlace de restablecimiento de contraseña
            string resetPasswordUrl = $"{URL_DESARROLLO}/TicketCR/RestablecerClave?email={HttpUtility.UrlEncode(emailAddress)}&token={HttpUtility.UrlEncode(recoveryToken)}";

            // URL del logo de TicketCR que viene  desde cloudinary
            string logoImageUrl = "https://res.cloudinary.com/dxhibktjk/image/upload/v1691118885/TicketCR/voqawn66pbpsc72x1ozw.png";

            // Crear el contenido del correo electrónico con el enlace HTML y la imagen del logo
            string emailContentHtml = $@"
                <html>
                <body style='text-align: center;'>
                    <table align='center' cellpadding='0' cellspacing='0' border='0'>
                        <tr>
                            <td>
                                <img src='{logoImageUrl}' alt='TicketCR' width='400' height='200'>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <p>Haga clic en el siguiente botón para restablecer su contraseña:</p>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <a style='display: inline-block; font-size: 16px; font-weight: bold; color: #ffffff; background-color: orange; padding: 10px 20px; text-decoration: none; border-radius: 5px; text-align: center;' href='{resetPasswordUrl}'>presione aquí</a>
                            </td>
                        </tr>
                    </table>
                </body>
                </html>";

            //TITULO DEL CORREO
            EmailContent emailContent = new EmailContent("Recuperación de contraseña");
            //SE AGREGA EL HTML EN EL CUERPO DEL CORREO
            emailContent.Html = emailContentHtml;

            List<EmailAddress> emailAddresses = new List<EmailAddress> { new EmailAddress(emailAddress, "Usuario de TicketCR") };
            EmailRecipients emailRecipients = new EmailRecipients(emailAddresses);
            EmailMessage emailMessage = new EmailMessage("confirmacion@12a64915-1302-4bb3-b6b1-6864fab7c925.azurecomm.net", emailRecipients, emailContent);
            EmailSendOperation emailSendOperation = await emailClient.SendAsync(
                                                    WaitUntil.Completed,
                                                    emailMessage, CancellationToken.None);
            EmailSendResult statusMonitor = emailSendOperation.Value;

            Console.WriteLine($"Email Sent. Status = {emailSendOperation.Value.Status}");

            return emailSendOperation.Value.Status.ToString();
        }
        #endregion


        #region 
        public async Task<string> SendEmailActivation(string emailAddress, int idUsuario)
        {
            OtpCrudFactory otpCrud = new OtpCrudFactory();
            if (!IsValidUser(emailAddress))
            {
                // Si el correo del usuario no es válido, se envia una excepción un mensaje de error de que el correo no es valido
                throw new ArgumentException("El correo del usuario no es válido.");
            }


            Otp otp = otpCrud.ObtenerOTPporIDUsuario(idUsuario);

            int otpUsuarioGenerado = otp.otp;

            string connectionString = "endpoint=https://ticketcr-communicationservice.unitedstates.communication.azure.com/;accesskey=xb5/aGM6Swnd/BYM6F+MRAL4Hk03ByUo4fYzQ+lhU0BltXIxrsy8VRutCCMWPEOzJv54ukEbmBA6fz6j7TleaA==";
            EmailClient emailClient = new EmailClient(connectionString);



            // URL del enlace de restablecimiento de contraseña
            string activacionUrl = $"{URL_DESARROLLO}/TicketCR/ValidacionCuenta?email={HttpUtility.UrlEncode(emailAddress)}&token={otpUsuarioGenerado}&idUsuario={idUsuario}";

            // URL del logo de TicketCR que viene  desde cloudinary
            string logoImageUrl = "https://res.cloudinary.com/dxhibktjk/image/upload/v1691118885/TicketCR/voqawn66pbpsc72x1ozw.png";

            // Crear el contenido del correo electrónico con el enlace HTML y la imagen del logo
            string emailContentHtml = $@"
                <html>
                <body style='text-align: center;'>
                    <table align='center' cellpadding='0' cellspacing='0' border='0'>
                        <tr>
                            <td>
                                <img src='{logoImageUrl}' alt='TicketCR' width='400' height='200'>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <p>Haga clic en el siguiente botón para activar su cuenta:</p>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <a style='display: inline-block; font-size: 16px; font-weight: bold; color: #ffffff; background-color: orange; padding: 10px 20px; text-decoration: none; border-radius: 5px; text-align: center;' href='{activacionUrl}'>presione aquí</a>
                            </td>
                        </tr>
                    </table>
                </body>
                </html>";

            //TITULO DEL CORREO
            EmailContent emailContent = new EmailContent("Validación de cuenta");
            //SE AGREGA EL HTML EN EL CUERPO DEL CORREO
            emailContent.Html = emailContentHtml;

            List<EmailAddress> emailAddresses = new List<EmailAddress> { new EmailAddress(emailAddress, "Usuario de TicketCR") };
            EmailRecipients emailRecipients = new EmailRecipients(emailAddresses);
            EmailMessage emailMessage = new EmailMessage("confirmacion@12a64915-1302-4bb3-b6b1-6864fab7c925.azurecomm.net", emailRecipients, emailContent);
            EmailSendOperation emailSendOperation = await emailClient.SendAsync(
                                                    WaitUntil.Completed,
                                                    emailMessage, CancellationToken.None);
            EmailSendResult statusMonitor = emailSendOperation.Value;

            Console.WriteLine($"Email Sent. Status = {emailSendOperation.Value.Status}");

            return emailSendOperation.Value.Status.ToString();
        }
        #endregion

        private bool IsValidUser(string emailAddress)
        {
            EmailCrudFactory emailCrudFactory = new EmailCrudFactory();
            Usuario usuario = emailCrudFactory.RetrieveByEmail<Usuario>(emailAddress);
            // Verificar si el usuario existe y el correo electrónico coincide exactamente con el que ingresa el usuario y el de la base de datos.
            return usuario != null && usuario.Correo == emailAddress;
        }



        public async Task<string> CorreoMembresiaAdquirida(string correo, string NombreMembresia, int IdNuevaMembresia, float CostoTotal, float Impuestos, int idUsuario, string IdPagoPaypal)
        {

            MembresiaManager ActualizarMembresia = new MembresiaManager();

            string connectionString = "endpoint=https://ticketcr-communicationservice.unitedstates.communication.azure.com/;accesskey=xb5/aGM6Swnd/BYM6F+MRAL4Hk03ByUo4fYzQ+lhU0BltXIxrsy8VRutCCMWPEOzJv54ukEbmBA6fz6j7TleaA==";
            EmailClient emailClient = new EmailClient(connectionString);

            // URL del logo de TicketCR que viene  desde cloudinary
            string logoImageUrl = "https://res.cloudinary.com/dxhibktjk/image/upload/v1691118885/TicketCR/voqawn66pbpsc72x1ozw.png";

            //numero factura
            string numerofactura = GenerarNumeroFactura();

            //Generar fecha con hora actual
            DateTime fechaActual = DateTime.Now;

            //Calcular sub
            float subtotal = CostoTotal / (1 + Impuestos / 100);

            float total = CostoTotal;

            //CREAR FACTURA PARA INSERTARLA EN LA COMPRAMEMBRESIA DE LA BASE DE DATOS


            ActualizarMembresia.ActualizarMembresiaUsuarioYRegistrarCompra(IdNuevaMembresia, CostoTotal, subtotal, Impuestos, idUsuario, IdPagoPaypal);


            // Crear el contenido del correo electrónico con el enlace HTML y la imagen del logo
            string emailContentHtml = $@"
                <html>
                <head>
                    <meta http-equiv='Content-Type' content='text/html; charset=utf-8'/>
                    <style type='text/css'>
        
                        .center-table {{
                            margin: 0 auto;
                        }}

                    </style>
                </head>
                <body style='text-align: center;'>
                    <div class='factura'>
                        <div class='encabezado'>
                            <img src='{logoImageUrl}' alt='Logo' class='logo'>
                            <div style='color:black;'>
                                <p>TicketCR</p>
                                <p style:><strong>Dirección:</strong> calle 87, avenida 46, Curridabat, San José</p>
                                <p><strong>Teléfono:</strong> 8888-8888</p>
                                <p><strong>Correo:</strong> ticketcr@gmail.com</p>
                            </div>
                        </div>
                        <div class='datos-factura'>
                            <h3>Factura Electrónica</h3>
                            <p><strong>Número de factura:</strong> {numerofactura}</p>
                            <p><strong>Fecha del servicio:</strong> {fechaActual}</p>
                            <p><strong>Fecha de pago:</strong> {fechaActual}</p>
                        </div>
                        <div style='text-align: center;'>
                            <table style='text-align: center; border-collapse:collapse; width: 100%; margin: 0 auto;'>
                                <thead>
                                    <tr>
                                        <th style='border: 1px solid #dddddd; background-color: #f2f2f2; padding: 8px;'>Detalle de la Membresía</th>
                                        <th style='border: 1px solid #dddddd; background-color: #f2f2f2; padding: 8px;'>Cantidad</th>
                                        <th style='border: 1px solid #dddddd; background-color: #f2f2f2; padding: 8px;'>Precio Unitario</th>
                                        <th style='border: 1px solid #dddddd; background-color: #f2f2f2; padding: 8px;'>Subtotal</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td style='border: 1px solid #dddddd; padding: 8px;'>{NombreMembresia}</td>
                                        <td style='border: 1px solid #dddddd; padding: 8px;'>{1}</td>
                                        <td style='border: 1px solid #dddddd; padding: 8px;'>${total}</td>
                                        <td style='border: 1px solid #dddddd; padding: 8px;'>${Math.Floor(subtotal)}</td>
                                    </tr>
                                </tbody>
                                <tfoot>
                                    <tr>
                                        <th colspan='3' style='border: 1px solid #dddddd; padding: 8px;'>Costo servicios</th>
                                        <td style='border: 1px solid #dddddd; padding: 8px;'>{Impuestos}%</td>
                                    </tr>
                                    <tr>
                                        <th colspan='3' style='border: 1px solid #dddddd; padding: 8px;'>Total Factura</th>
                                        <td style='border: 1px solid #dddddd; padding: 8px;'>${total}</td>
                                    </tr>
                                </tfoot>
                            </table>
                        </div>
                        <div class='cond'>
                            <div class='col-12 mt-3'>
                                <h4>Condiciones y formas de pago</h4>
                                <p>
                                    Paypal
                                    <br /> Numero de pago: {IdPagoPaypal}
                                </p>
                            </div>
                        </div>
                    </div>
                </body>
                </html>
                ";

            //TITULO DEL CORREO
            EmailContent emailContent = new EmailContent("Membresia adquirida");
            //SE AGREGA EL HTML EN EL CUERPO DEL CORREO
            emailContent.Html = emailContentHtml;

            List<EmailAddress> emailAddresses = new List<EmailAddress> { new EmailAddress(correo, "Usuario de TicketCR") };

            EmailRecipients emailRecipients = new EmailRecipients(emailAddresses);
            EmailMessage emailMessage = new EmailMessage("confirmacion@12a64915-1302-4bb3-b6b1-6864fab7c925.azurecomm.net", emailRecipients, emailContent);
            EmailSendOperation emailSendOperation = await emailClient.SendAsync(
                                                    WaitUntil.Completed,
                                                    emailMessage, CancellationToken.None);
            EmailSendResult statusMonitor = emailSendOperation.Value;

            Console.WriteLine($"Email Sent. Status = {emailSendOperation.Value.Status}");

            return emailSendOperation.Value.Status.ToString();
        }

        public async Task<string> CorreoFacturaBoletos(List<CompraBoletosPresenciales> boletosComprados, string cuentaPaypal)
        {


            string connectionString = "endpoint=https://ticketcr-communicationservice.unitedstates.communication.azure.com/;accesskey=xb5/aGM6Swnd/BYM6F+MRAL4Hk03ByUo4fYzQ+lhU0BltXIxrsy8VRutCCMWPEOzJv54ukEbmBA6fz6j7TleaA==";

            EmailClient emailClient = new EmailClient(connectionString);

            // URL del logo de TicketCR que viene  desde cloudinary
            string logoImageUrl = "https://res.cloudinary.com/dxhibktjk/image/upload/v1691118885/TicketCR/voqawn66pbpsc72x1ozw.png";

            //numero factura
            string numerofactura = GenerarNumeroFactura();

            //Generar fecha con hora actual
            DateTime fechaActual = DateTime.Now;

            string boletosTableHtml = string.Join("", boletosComprados.Select(boleto => $@"

                <tr>
                    <td style='border: 1px solid #dddddd; padding: 8px;'>{boleto.NombreEventoAsistir}</td>
                    <td style='border: 1px solid #dddddd; padding: 8px;'>{boleto.CantidadBoletos}</td>
                    <td style='border: 1px solid #dddddd; padding: 8px;'>${boleto.Total}</td>
                    <td style='border: 1px solid #dddddd; padding: 8px;'>${boleto.Subtotal}</td>
                </tr>
            "));

            string correo = boletosComprados.First().Correo;
            float costoServicios = boletosComprados.First().Comision;
            float total = boletosComprados.First().Total;
            string nombreEvento = boletosComprados.First().NombreEventoAsistir;

            // Crear el contenido del correo electrónico con el enlace HTML y la imagen del logo
            string emailContentHtml = $@"
            <html>
            <head>
                <meta http-equiv='Content-Type' content='text/html; charset=utf-8'/>
                <style type='text/css'>
                    .center-table {{
                        margin: 0 auto;
                    }}
                </style>
            </head>
            <body style='text-align: center;'>
                <div class='factura'>
                    <div class='encabezado'>
                        <img src='{logoImageUrl}' alt='Logo' class='logo'>
                        <div style='color:black;'>
                            <p>TicketCR</p>
                            <p><strong>Dirección:</strong> calle 87, avenida 46, Curridabat, San José</p>
                            <p><strong>Teléfono:</strong> 8888-8888</p>
                            <p><strong>Correo:</strong> ticketcr@gmail.com</p>
                        </div>
                    </div>
                    <div class='datos-factura'>
                        <h3>Factura Electrónica</h3>
                        <p><strong>Número de factura:</strong> {numerofactura}</p>
                        <p><strong>Fecha del servicio:</strong> {fechaActual}</p>
                        <p><strong>Fecha de pago:</strong> {fechaActual}</p>
                    </div>
                    <div style='text-align: center;'>
                        <table style='text-align: center; border-collapse:collapse; width: 100%; margin: 0 auto;'>
                            <thead>
                                <tr>
                                    <th style='border: 1px solid #dddddd; background-color: #f2f2f2; padding: 8px;'>Detalle de compra</th>
                                    <th style='border: 1px solid #dddddd; background-color: #f2f2f2; padding: 8px;'>Cantidad</th>
                                    <th style='border: 1px solid #dddddd; background-color: #f2f2f2; padding: 8px;'>Precio Unitario</th>
                                    <th style='border: 1px solid #dddddd; background-color: #f2f2f2; padding: 8px;'>Subtotal</th>
                                </tr>
                            </thead>
                            <tbody>
                                {boletosTableHtml}
                            </tbody>
                            <tfoot>
                                <tr>
                                    <th colspan='3' style='border: 1px solid #dddddd; padding: 8px;'>Costo servicios</th>
                                    <td style='border: 1px solid #dddddd; padding: 8px;'>%{costoServicios}</td>
                                </tr>
                                <tr>
                                    <th colspan='3' style='border: 1px solid #dddddd; padding: 8px;'>Total Factura</th>
                                    <td style='border: 1px solid #dddddd; padding: 8px;'>${total}</td>
                                </tr>
                            </tfoot>
                        </table>
                    </div>
                    <div class='cond'>
                        <div class='col-12 mt-3'>
                            <h4>Condiciones y formas de pago</h4>
                            <p>
                                Paypal
                                <br /> Numero de pago: {cuentaPaypal}
                            </p>
                        </div>
                    </div>
                </div>
            </body>
            </html>
        ";



            //TITULO DEL CORREO
            EmailContent emailContent = new EmailContent($"Factura electrónica boletos comprados para {nombreEvento}");
            //SE AGREGA EL HTML EN EL CUERPO DEL CORREO
            emailContent.Html = emailContentHtml;

            List<EmailAddress> emailAddresses = new List<EmailAddress> { new EmailAddress(correo, "Usuario comprador de TicketCR") };

            EmailRecipients emailRecipients = new EmailRecipients(emailAddresses);
            EmailMessage emailMessage = new EmailMessage("confirmacion@12a64915-1302-4bb3-b6b1-6864fab7c925.azurecomm.net", emailRecipients, emailContent);
            EmailSendOperation emailSendOperation = await emailClient.SendAsync(
                                                    WaitUntil.Completed,
                                                    emailMessage, CancellationToken.None);
            EmailSendResult statusMonitor = emailSendOperation.Value;

            Console.WriteLine($"Email Sent. Status = {emailSendOperation.Value.Status}");

            return emailSendOperation.Value.Status.ToString();
        }

        public string GenerarNumeroFactura()
        {
            Random random = new Random();
            int provinceCode = random.Next(1, 7); // Código de provincia aleatorio (1-7)
            int consecutiveNumber = random.Next(1, 10000); // Número consecutivo aleatorio
            string currentDate = DateTime.Now.ToString("ddMMyy"); // Fecha actual en formato ddmmyy

            string invoiceNumber = $"{provinceCode:D3}-{consecutiveNumber:D4}-{currentDate}";
            return invoiceNumber;
        }

        public async Task<string> CorreosCompraBoletosVirtuales(List<CompraBoletosVirtuales> compraBoletos)
        {
            CompraBoletosManager comp = new CompraBoletosManager();

            foreach (var boleto in compraBoletos)
            {
                comp.InsertarCompraBoletosVirtuales(boleto);
            }

            string connectionString = "endpoint=https://ticketcr-communicationservice.unitedstates.communication.azure.com/;accesskey=xb5/aGM6Swnd/BYM6F+MRAL4Hk03ByUo4fYzQ+lhU0BltXIxrsy8VRutCCMWPEOzJv54ukEbmBA6fz6j7TleaA==";
            EmailClient emailClient = new EmailClient(connectionString);

            string logoImageUrl = "https://res.cloudinary.com/dxhibktjk/image/upload/v1691118885/TicketCR/voqawn66pbpsc72x1ozw.png";

            foreach (var compra in compraBoletos)
            {

                string correoUsuario = compra.Correo;

                string emailContentHtml = $@"
                <!DOCTYPE html>
                <html>
                <head>
                    <title>Confirmación de compra de boletos</title>
                </head>
                <body>
                    <div style='max-width: 600px; margin: 0 auto; padding: 20px; background-color: #ffffff; box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);'>
                        <img src='{logoImageUrl}' alt='Logo' style='max-width: 100px; margin: 0 auto; display: block;'>
                        <div style='text-align: center; margin-top: 20px;'>
                            <h1>¡Tus boletos han sido comprados exitosamente!</h1>
                            <p>Detalles de la compra:</p>
                        </div>
                        <div style='margin-top: 20px;'>
                            <div style='padding: 10px; background-color: #f4f4f4; border-radius: 5px;'>
                                <p><strong>Evento:</strong> {compra.NombreEventoAsistir}</p>
                                <p><strong>Cantidad de boletos:</strong> {compra.CantidadBoletos}</p>
                                <p><strong>Fecha inicio del evento</strong> {compra.FechaInicio}</p>
                                <p><strong>Fecha final del evento</strong> {compra.FechaFinal}</p>
                                <p><strong>Total a pagar:</strong> ${compra.Total}</p>
                                <p><strong>Detalles de los boletos:</strong></p>
                                <ul>
                                    <li>Cantidad: {compra.CantidadBoletos}</li>
                                    <li>Subtotal: ${compra.Subtotal}</li>
                                    <li>Comisión: ${compra.Comision}</li>
                                    <li>Impuesto: ${compra.Impuesto}</li>
                                </ul>
                                <p><strong>Link para el ingreso al evento:</strong> {compra.Link}</p>
                            </div>
                        </div>
                    </div>
                </body>
                </html>";



                EmailContent emailContent = new EmailContent($"Boletos comprados para {compra.NombreEventoAsistir}");
                emailContent.Html = emailContentHtml;

                List<EmailAddress> emailAddresses = new List<EmailAddress> { new EmailAddress(correoUsuario, "Usuario TicketCR") };

                EmailRecipients emailRecipients = new EmailRecipients(emailAddresses);

                EmailMessage emailMessage = new EmailMessage("confirmacion@12a64915-1302-4bb3-b6b1-6864fab7c925.azurecomm.net", emailRecipients, emailContent);

                EmailSendOperation emailSendOperation = await emailClient.SendAsync(WaitUntil.Completed, emailMessage, CancellationToken.None);
                EmailSendResult statusMonitor = emailSendOperation.Value;

                Console.WriteLine($"Email Sent to {correoUsuario}. Status = {emailSendOperation.Value.Status}");

            }

            return "Correos enviados con exito";
        }

        public async Task<string> CorreosCompraBoletosPresenciales(List<CompraBoletosPresenciales> compraBoletos)
        {
            CompraBoletosManager comp = new CompraBoletosManager();

            string csImages = "DefaultEndpointsProtocol=https;AccountName=ticketcr;AccountKey=K+2cSvHat/J63nmbYsjXRRGLbuAY0OSdGF6kRQGGC/CBAiotkl9qiCBUM/Z0TB8cCb+w5SnxPDuu+AStd2yMwQ==;EndpointSuffix=core.windows.net";
            string containerName = "containerqr";
            BlobServiceClient blobServiceClient = new BlobServiceClient(csImages);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);

            foreach (var boleto in compraBoletos)
            {
                comp.InsertarCompraBoletosPresenciales(boleto);
            }

      
            string connectionString = "endpoint=https://ticketcr-communicationservice.unitedstates.communication.azure.com/;accesskey=xb5/aGM6Swnd/BYM6F+MRAL4Hk03ByUo4fYzQ+lhU0BltXIxrsy8VRutCCMWPEOzJv54ukEbmBA6fz6j7TleaA==";
            EmailClient emailClient = new EmailClient(connectionString);

            string logoImageUrl = "https://res.cloudinary.com/dxhibktjk/image/upload/v1691118885/TicketCR/voqawn66pbpsc72x1ozw.png";

            foreach (var compra in compraBoletos)
            {
                string UrlvalidarQr = $"{URL_DESARROLLO}/TicketCR/ValidarBoleto?codigoQr={HttpUtility.UrlEncode(compra.CodigoQr)}";

                string codigoGenerado = UrlvalidarQr;
                AdminQR adQr = new AdminQR();
                byte[] imagen = adQr.GenerateQR(codigoGenerado);
                string blopQr = compra.CodigoQr+".png";
                await containerClient.DeleteBlobIfExistsAsync(blopQr);
                blopQr = compra.CodigoQr + ".png";
                using (MemoryStream qrStream = new MemoryStream(imagen))
                {
                    await containerClient.UploadBlobAsync(blopQr, qrStream);
                }
                string qrImageUrl = containerClient.Uri + "/" + blopQr;

                string correoUsuario = compra.Correo;

                string emailContentHtml = $@"
                <!DOCTYPE html>
                <html>
                <head>
                    <title>Confirmación de compra de boletos</title>
                </head>
                <body>
                    <div style='max-width: 600px; margin: 0 auto; padding: 20px; background-color: #ffffff; box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);'>
                        <img src='{logoImageUrl}' alt='Logo' style='max-width: 100px; margin: 0 auto; display: block;'>
                        <div style='text-align: center; margin-top: 20px;'>
                            <h1>¡Tus boletos han sido comprados exitosamente!</h1>
                            <p>Detalles de la compra:</p>
                        </div>
                        <div style='margin-top: 20px;'>
                            <div style='padding: 10px; background-color: #f4f4f4; border-radius: 5px;'>
                                <p><strong>Evento:</strong> {compra.NombreEventoAsistir}</p>
                                <p><strong>Asiento:</strong> {compra.Asiento}</p>
                                <p><strong>Cantidad de boletos:</strong> {compra.CantidadBoletos}</p>
                                <p><strong>Fecha inicio del evento</strong> {compra.FechaInicio}</p>
                                <p><strong>Fecha final del evento</strong> {compra.FechaFinal}</p>
                                <p><strong>Total a pagar:</strong> ${compra.Total}</p>
                                <p><strong>Detalles de los boletos:</strong></p>
                                <ul>
                                    <li>Cantidad: {compra.CantidadBoletos}</li>
                                    <li>Subtotal: ${compra.Subtotal}</li>
                                    <li>Comisión: ${compra.Comision}</li>
                                    <li>Impuesto: ${compra.Impuesto}</li>
                                </ul>
                             <p style='text-align: center;'>Código QR para el ingreso al evento:</p>
                             <img src='{qrImageUrl}' style='display: block; margin: 0 auto;'>
                          </div>
                        </div>
                    </div>
                </body>
                </html>";

                EmailContent emailContent = new EmailContent($"Boletos comprados para {compra.NombreEventoAsistir}");
                emailContent.Html = emailContentHtml;

                List<EmailAddress> emailAddresses = new List<EmailAddress> { new EmailAddress(correoUsuario, "Usuario TicketCR") };

                EmailRecipients emailRecipients = new EmailRecipients(emailAddresses);

                EmailMessage emailMessage = new EmailMessage("confirmacion@12a64915-1302-4bb3-b6b1-6864fab7c925.azurecomm.net", emailRecipients, emailContent);

                EmailSendOperation emailSendOperation = await emailClient.SendAsync(WaitUntil.Completed, emailMessage, CancellationToken.None);
                EmailSendResult statusMonitor = emailSendOperation.Value;

                Console.WriteLine($"Email Sent to {correoUsuario}. Status = {emailSendOperation.Value.Status}");

            }

            return "Correos enviados con exito";
        }

    }
}



