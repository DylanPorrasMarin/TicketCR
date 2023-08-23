using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection;
using TicketCR_UI.Models;

namespace TicketCR_UI.Controllers
{
    public class TicketCRController : Controller
    {
      
        //USAR PARA PRODUCCION: 
        //private const string BASE_URL = "https://ticketcr-api.azurewebsites.net";

        //USAR LOCAL: 
        private const string BASE_URL = "https://localhost:7255";

        public IActionResult LandingTicketCR()
        {
            return View();
        }

        protected UsuarioModel ObtenerUsuarioSession()
        {
            // Obtener el usuario de la sesión
            UsuarioModel usuarioSession = new UsuarioModel
            {
                IdUsuario = HttpContext.Session.GetInt32("idUser") ?? 0,
                Rol = HttpContext.Session.GetString("userRol")
                // Agrega aquí los demás atributos del modelo de usuario que necesites

            };

            return usuarioSession;
        }

        public IActionResult MenuEventosUsuario()
        {

            List<EventoModel> listaEventos = new List<EventoModel>();
            var urlMethod = string.Format("/api/Evento/ObtenerEventoVigentes");
            var finalUrl = BASE_URL + urlMethod;

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(finalUrl);

            try
            {
                var result = client.GetAsync(finalUrl).Result;

                if (result.IsSuccessStatusCode)
                {
                    // Atributos desde la bd por medio de la api
                    var json = result.Content.ReadAsStringAsync().Result;
                    listaEventos = JsonConvert.DeserializeObject<List<EventoModel>>(json);
                    
                    }
         
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Ocurrió un error al enviar la solicitud: " + ex.Message;
                return View();
            }



            // Pasar la lista de eventos a la vista MenuEventosUsuario
            return View(listaEventos);
        }

        //recibe el id que se le pasa por parametro en el cshtml
        public async Task<IActionResult> ComprarBoletos(int id)
        {
            EventoModel evento = new EventoModel();

            var urlEvento = string.Format("/api/Evento/ObtenerEventoYCreador?idEvento={0}", id);
            var urlBoletosEvento = string.Format("/api/Boleto/ObtenerBoletosEvento?idEvento={0}", id);
            var finalUrl = BASE_URL;

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(finalUrl);

            // Obtener información del evento
            var resultEvento = await client.GetAsync(urlEvento);

            if (resultEvento.IsSuccessStatusCode)
            {
                var jsonEvento = await resultEvento.Content.ReadAsStringAsync();
                evento = JsonConvert.DeserializeObject<EventoModel>(jsonEvento);
            }

            // Obtener la lista de boletos del evento
            var resultBoletos = await client.GetAsync(urlBoletosEvento);

            if (resultBoletos.IsSuccessStatusCode)
            {
                var jsonBoletos = await resultBoletos.Content.ReadAsStringAsync();
                evento.Boletos = JsonConvert.DeserializeObject<List<BoletoModel>>(jsonBoletos);
            }

            // Crear una lista auxiliar para almacenar los tipos únicos de boletos
            // Obtener los tipos únicos de boletos con estado true
            var tiposUnicosBoletos = evento.Boletos
             .Where(boleto => boleto.EstadoComprado == false)
             .Select(boleto => boleto.TipoBoleto)
             .Distinct()
             .ToList();

            // Crear una lista de objetos Boleto correspondientes a los tipos únicos
            var boletosUnicos = tiposUnicosBoletos
                .Select(tipoBoleto => evento.Boletos.FirstOrDefault(boleto => boleto.TipoBoleto == tipoBoleto && boleto.EstadoComprado == false))
                .ToList();

            ViewData["BoletosUnicos"] = boletosUnicos;


            // Puedes ordenar los tipos de boletos si lo deseas
            tiposUnicosBoletos.Sort();

            // Pasar la lista de tipos únicos de boletos a la vista
            ViewData["TiposUnicosBoletos"] = tiposUnicosBoletos;

            return View(evento);
        }
        // Función para obtener la descripción del tipo de boleto
        public string ObtenerDescripcionTipoBoleto(int tipoBoleto)
        {
            switch (tipoBoleto)
            {
                case 1:
                    return "Particular";
                case 2:
                    return "Estudiante";
                case 3:
                    return "Adulto mayor";
                case 4:
                    return "Regalia";
                default:
                    return "Desconocido";
            }
        }

		public IActionResult ValidacionCuenta()
		{
			return View();
		}
		public IActionResult Registro()
        {
            return View();
        }
        public IActionResult MenuEventos()
        {
            List<EventoModel> listaEventos = new List<EventoModel>();
            var urlMethod = string.Format("/api/Evento/ObtenerEventoVigentes");
            var finalUrl = BASE_URL + urlMethod;
            UsuarioModel usuario = ObtenerUsuarioSession();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(finalUrl);

            try
            {
                var result = client.GetAsync(finalUrl).Result;

                if (result.IsSuccessStatusCode)
                {
                    // Atributos desde la bd por medio de la api
                    var json = result.Content.ReadAsStringAsync().Result;
                    listaEventos = JsonConvert.DeserializeObject<List<EventoModel>>(json);

                }

            }
            catch (Exception ex)
            {
                ViewBag.Message = "Ocurrió un error al enviar la solicitud: " + ex.Message;
                return View();
            }



            // Pasar la lista de eventos a la vista MenuEventosUsuario
            return View(listaEventos);
        }
        public IActionResult HistorialEventos()
        {
            List<EventoModel> listaEventos = new List<EventoModel>();
            UsuarioModel usuario = ObtenerUsuarioSession();
            var urlMethod = string.Format("/api/Evento/ObtenerHistorialEventos?idUsuario={0}", usuario.IdUsuario);
            var finalUrl = BASE_URL + urlMethod;

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(finalUrl);

            try
            {
                var result = client.GetAsync(finalUrl).Result;

                if (result.IsSuccessStatusCode)
                {
                    // Atributos desde la bd por medio de la api
                    var json = result.Content.ReadAsStringAsync().Result;
                    listaEventos = JsonConvert.DeserializeObject<List<EventoModel>>(json);

                }

            }
            catch (Exception ex)
            {
                ViewBag.Message = "Ocurrió un error al enviar la solicitud: " + ex.Message;
                return View();
            }
            


            return View(listaEventos);
        }

        public IActionResult InformeEventos()
        {
            List<InformeEventosModel> listaEventos = new List<InformeEventosModel>();
            UsuarioModel usuario = ObtenerUsuarioSession();

            if (usuario.Rol == "1")
            {
                var urlMethod = string.Format("/api/InformeEventos/ObtenerInformeEventos");
                var finalUrl = BASE_URL + urlMethod;

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(finalUrl);

                try
                {
                    var result = client.GetAsync(finalUrl).Result;

                    if (result.IsSuccessStatusCode)
                    {
                        // Atributos desde la bd por medio de la api
                        var json = result.Content.ReadAsStringAsync().Result;
                        listaEventos = JsonConvert.DeserializeObject<List<InformeEventosModel>>(json);

                    }

                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Ocurrió un error al enviar la solicitud: " + ex.Message;
                    return View();
                }



            }
            else
            {

                var urlMethod = string.Format("/api/InformeEventos/ObtenerInformeEventosGestor?idUsuario={0}", usuario.IdUsuario);
                var finalUrl = BASE_URL + urlMethod;

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(finalUrl);

                try
                {
                    var result = client.GetAsync(finalUrl).Result;

                    if (result.IsSuccessStatusCode)
                    {
                        // Atributos desde la bd por medio de la api
                        var json = result.Content.ReadAsStringAsync().Result;
                        listaEventos = JsonConvert.DeserializeObject<List<InformeEventosModel>>(json);

                    }

                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Ocurrió un error al enviar la solicitud: " + ex.Message;
                    return View();
                }

            }

            return View(listaEventos);
        }

        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Logout()
        {

            HttpContext.Session.Clear(); //Limpia la sesion
            return RedirectToAction("LandingTicketCR", "TicketCR");  //redirige al index home 
        }

        public IActionResult Cancel()
        {
            HttpContext.Session.Clear(); //Limpia la sesion
            return RedirectToAction("Index", "Home");  //redirige al index home 
        }

        // Controller de inicio de sesion
        #region Seccion Login
        [HttpPost]
        public IActionResult Login(Usuario user)
        {
            var urlMethod = string.Format("/api/Login/ValidateUser?correo={0}&clave={1}", user.Correo, user.Clave);
            var finalUrl = BASE_URL + urlMethod;

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(finalUrl);

            try
            {
                var result = client.GetAsync(finalUrl).Result;

                if (result.IsSuccessStatusCode)
                {
                    // Atributos desde la bd por medio de la api
                    var json = result.Content.ReadAsStringAsync().Result;
                    var jsonObject = JsonConvert.DeserializeObject<Usuario>(json);
                    // Atributos desde la bd por medio de la api
                    var userBD = jsonObject.Correo.Trim();
                    var passwordBD = jsonObject.Clave.Trim();
                    var nameUser = jsonObject.Nombre.Trim();
                    var userApellidos = jsonObject.Apellidos.Trim();
                    var userRol = jsonObject.Rol.Trim();
//nuevos
                     var userTelefono = jsonObject.Telefono.Trim();
                     var userCedula = jsonObject.Cedula.Trim();
                     var userCorreo = jsonObject.Correo.Trim();
                     var userLongitud = jsonObject.Longitud.ToString();
                     var userLatitud = jsonObject.Latitud.ToString();
                     var userFechaNacimiento = jsonObject.FechaNacimiento.ToString("yyyy-MM-dd");
                     var userClave = jsonObject.Clave.Trim();
                     var idUser = jsonObject.IdUsuario;
					 var idMembresiaUser = jsonObject.IdMembresia;
                     var cantidadEventos = jsonObject.CantidadEventos;

                    var estadoUser = jsonObject.Estado;

                    if (estadoUser == false)
                    {

                      ViewBag.Message = "Cuenta sin validación, intente de nuevo: ";
                       return View();
                    }
                    else {

                        if (user.Correo == userBD && user.Clave == passwordBD)
                        {
                            HttpContext.Session.SetString("user", nameUser + " " + userApellidos);

                            HttpContext.Session.SetString("userRol", userRol);

                            //nuevos
                            HttpContext.Session.SetInt32("idUser", idUser);
                            HttpContext.Session.SetInt32("cantidadEventos", cantidadEventos);
                            HttpContext.Session.SetInt32("idMembresia", idMembresiaUser);
                            HttpContext.Session.SetString("nameUser", nameUser);
                            HttpContext.Session.SetString("userApellidos", userApellidos);
                            HttpContext.Session.SetString("userTelefono", userTelefono);
                            HttpContext.Session.SetString("userCedula", userCedula);
                            HttpContext.Session.SetString("userCorreo", userCorreo);
                            HttpContext.Session.SetString("userLongitud", userLongitud);
                            HttpContext.Session.SetString("userLatitud", userLatitud);
                            HttpContext.Session.SetString("userFechaNacimiento", userFechaNacimiento);
                            HttpContext.Session.SetString("userClave", userClave);

                            UsuarioModel usuarioSession = new UsuarioModel
                            {
                                IdUsuario = idUser,
                                Rol = userRol

                            };

                            if (userRol == "1")
                            { return RedirectToAction("InformeEventos", "TicketCR"); }
                            else if (userRol == "2")
                            { return RedirectToAction("InformeEventos", "TicketCR"); }
                            else if (userRol == "3")
                            { return RedirectToAction("MenuEventos", "TicketCR", usuarioSession); }
                        }
                    }
                }

					
                ViewBag.Message = "Correo y/o contraseña incorrectos";
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Ocurrió un error al enviar la solicitud: " + ex.Message;
                return View();
            }
        }

        //FIN CONTROLLER INICIO SESION
        #endregion


        public IActionResult RecuperarClave()
        {
            return View();
        }

        public IActionResult RestablecerClave()
        {
            return View();
        }

        public IActionResult InterfazMembresia()
        {
            return View();
        }

        public IActionResult AdministrarUsuarios()
        {
            List<UsuarioModel> usuarios = new List<UsuarioModel>();
            var urlMethod = string.Format("/api/Usuario/ObtenerUsuariosRegistrados");//PONER LA API QUE SE CREA DEL LISTAR USUARIOS
            var finalUrl = BASE_URL + urlMethod;

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(finalUrl);

            var result = client.GetAsync(finalUrl).Result;

            if (result.IsSuccessStatusCode)
            {
                var json = result.Content.ReadAsStringAsync().Result;
                usuarios = JsonConvert.DeserializeObject<List<UsuarioModel>>(json);


            }

            return View(usuarios);
        }
        public IActionResult Escenario1()
        {
            return View();
        }
        public IActionResult Escenario2()
        {
            return View();
        }

        public IActionResult PerfilUsuario()
        {
            return View();
        }

        public IActionResult AdquirirMembresia()
        {
            UsuarioModel usuarioSesion = ObtenerUsuarioSession();
            List<MembresiaModel> membresias = new List<MembresiaModel>(); // Cambio el nombre a 'membresias'

            var urlMethodMembresias = "/api/Membresia/ObtenerMembresias";
            var urlMethodUsuario = $"/api/Usuario/ObtenerUsuarioPorId?id={usuarioSesion.IdUsuario}";

            var urlMembresias = BASE_URL + urlMethodMembresias;
            var urlUsuario = BASE_URL + urlMethodUsuario;

            using (HttpClient client = new HttpClient())
            {
                var resultMembresias = client.GetAsync(urlMembresias).Result;
                if (resultMembresias.IsSuccessStatusCode)
                {
                    var json = resultMembresias.Content.ReadAsStringAsync().Result;
                    membresias = JsonConvert.DeserializeObject<List<MembresiaModel>>(json);
                }

                var resultUsuario = client.GetAsync(urlUsuario).Result;
                if (resultUsuario.IsSuccessStatusCode)
                {
                    var json = resultUsuario.Content.ReadAsStringAsync().Result;
                    usuarioSesion = JsonConvert.DeserializeObject<UsuarioModel>(json);
                }
            }

            // Asignar la lista de membresías al usuario
            foreach (var membresia in membresias)
            {
                membresia.IdMembresiaUsuario = usuarioSesion.IdMembresia;
            }

            return View(membresias); // Pasar la lista de membresías actualizada a la vista
        }


        public IActionResult AdministradorCrearUsuarios()
        {
            return View();
        }
        public IActionResult CrearEvento()
        {

            UsuarioModel usuarioSesion = ObtenerUsuarioSession();

            var urlMethod = string.Format("/api/Usuario/ObtenerUsuarioPorId?id={0}", usuarioSesion.IdUsuario);//PONER LA API QUE SE CREA DEL LISTAR USUARIOS
            var finalUrl = BASE_URL + urlMethod;

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(finalUrl);

            var result = client.GetAsync(finalUrl).Result;

            if (result.IsSuccessStatusCode)
            {
                var json = result.Content.ReadAsStringAsync().Result;
                usuarioSesion = JsonConvert.DeserializeObject<UsuarioModel>(json);


            }

            return View(usuarioSesion);

        }

        public IActionResult Factura()
        {
            return View();
        }
        public IActionResult Parametros()
        {
            return View();
        }
        public IActionResult ConfiguracionEventos() 
        {
            List<EventoModel> listaEventos = new List<EventoModel>();
            UsuarioModel usuario = ObtenerUsuarioSession();
            if (usuario.Rol == "1")
            {
                
                var urlMethod = string.Format("/api/Evento/ObtenerEventoVigentes");
                var finalUrl = BASE_URL + urlMethod;

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(finalUrl);

                try
                {
                    var result = client.GetAsync(finalUrl).Result;

                    if (result.IsSuccessStatusCode)
                    {
                        // Atributos desde la bd por medio de la api
                        var json = result.Content.ReadAsStringAsync().Result;
                        listaEventos = JsonConvert.DeserializeObject<List<EventoModel>>(json);

                    }

                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Ocurrió un error al enviar la solicitud: " + ex.Message;
                    return View();
                }

            }
            else {

                var urlMethod = string.Format("/api/Evento/ObtenerEventosUsuario?idUsuario={0}",usuario.IdUsuario);
                var finalUrl = BASE_URL + urlMethod;

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(finalUrl);

                try
                {
                    var result = client.GetAsync(finalUrl).Result;

                    if (result.IsSuccessStatusCode)
                    {
                        // Atributos desde la bd por medio de la api
                        var json = result.Content.ReadAsStringAsync().Result;
                        listaEventos = JsonConvert.DeserializeObject<List<EventoModel>>(json);

                    }

                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Ocurrió un error al enviar la solicitud: " + ex.Message;
                    return View();
                }

            }

            return View(listaEventos);
        }
     

        public IActionResult InformeGanancias()
        {
            List<InformeMembresiasModel> listaMembresias = new List<InformeMembresiasModel>();

            var urlMethod = string.Format("/api/InformeEventos/ObtenerInformeMembresias");
            var finalUrl = BASE_URL + urlMethod;

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(finalUrl);

            try
            {
                var result = client.GetAsync(finalUrl).Result;

                if (result.IsSuccessStatusCode)
                {
                    // Atributos desde la bd por medio de la api
                    var json = result.Content.ReadAsStringAsync().Result;
                    listaMembresias = JsonConvert.DeserializeObject<List<InformeMembresiasModel>>(json);

                }

            }
            catch (Exception ex)
            {
                ViewBag.Message = "Ocurrió un error al enviar la solicitud: " + ex.Message;
                return View();
            }

            return View(listaMembresias);
        }

        public async Task<IActionResult> EditarEvento(int Id)
        {
            EventoModel evento = new EventoModel();

            var urlEvento = string.Format("/api/Evento/ObtenerEventoYCreador?idEvento={0}", Id);
            var urlBoletosEvento = string.Format("/api/Boleto/ObtenerBoletosEvento?idEvento={0}", Id);
            var finalUrl = BASE_URL;

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(finalUrl);

            // Obtener información del evento
            var resultEvento = await client.GetAsync(urlEvento);

            if (resultEvento.IsSuccessStatusCode)
            {
                var jsonEvento = await resultEvento.Content.ReadAsStringAsync();
                evento = JsonConvert.DeserializeObject<EventoModel>(jsonEvento);
            }

            // Obtener la lista de boletos del evento
            var resultBoletos = await client.GetAsync(urlBoletosEvento);

            if (resultBoletos.IsSuccessStatusCode)
            {
                var jsonBoletos = await resultBoletos.Content.ReadAsStringAsync();
                evento.Boletos = JsonConvert.DeserializeObject<List<BoletoModel>>(jsonBoletos);
            }

            // Crear una lista auxiliar para almacenar los tipos únicos de boletos
     

            // Pasar la lista de tipos únicos de boletos a la vista
  
            return View(evento);

        }

       public IActionResult ValidarBoleto()
        {

            return View();
        }

        public IActionResult AdministradorEditarUsuario(int Id) 
        {
            UsuarioModel usuarioElegido = new UsuarioModel();

            var urlMethod = string.Format("/api/Usuario/ObtenerUsuarioPorId?id={0}",Id);//PONER LA API QUE SE CREA DEL LISTAR USUARIOS
            var finalUrl = BASE_URL + urlMethod;

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(finalUrl);

            var result = client.GetAsync(finalUrl).Result;

            if (result.IsSuccessStatusCode)
            {
                var json = result.Content.ReadAsStringAsync().Result;
                usuarioElegido = JsonConvert.DeserializeObject<UsuarioModel>(json);


            }

            return View(usuarioElegido);
        }


        public IActionResult MembresiasCreadas()
        {
            List<MembresiaModel> membresias = new List<MembresiaModel>(); // Cambio el nombre a 'membresias'

            var urlMethodMembresias = "/api/Membresia/ObtenerMembresias";


            var urlMembresias = BASE_URL + urlMethodMembresias;

            using (HttpClient client = new HttpClient())
            {
                var resultMembresias = client.GetAsync(urlMembresias).Result;
                if (resultMembresias.IsSuccessStatusCode)
                {
                    var json = resultMembresias.Content.ReadAsStringAsync().Result;
                    membresias = JsonConvert.DeserializeObject<List<MembresiaModel>>(json);
                }

                return View(membresias);
            }
        }
    }
}
