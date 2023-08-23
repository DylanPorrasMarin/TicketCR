using DataAccess.Dao;
using DTO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mapper
{
    public class LoginMapper : ICrudStatements, IObjectMapper
    {
        public SqlOperation GetRetrieveByEmailStatement(string Email)
        {
            throw new NotImplementedException();
        }


        public SqlOperation GetValidateUserStatement(string correo, string clave)
        {
            var operation = new SqlOperation()
            {
                ProcedureName = "BuscarUsuarioPorCorreoYClave"
            };
            operation.AddVarcharParam("Correo", correo);
            operation.AddVarcharParam("Clave", clave);

            return operation;
        }

        public Usuario MapObjectFromResult(Dictionary<string, object> result)
        {
            Usuario user = new Usuario();
            if (result.ContainsKey("idUsuario"))
                user.IdUsuario = int.Parse(result["idUsuario"].ToString());

            if (result.ContainsKey("nombre"))
                user.Nombre = result["nombre"].ToString().Trim();
            if (result.ContainsKey("apellidos"))
                user.Apellidos = result["apellidos"].ToString().Trim();
            if (result.ContainsKey("cedula"))
                user.Cedula = result["cedula"].ToString().Trim();
            if (result.ContainsKey("clave"))
                user.Clave = result["clave"].ToString().Trim();
            if (result.ContainsKey("correo"))
                user.Correo = result["correo"].ToString().Trim();
            if (result.ContainsKey("telefono"))
                user.Telefono = result["telefono"].ToString().Trim();
            if (result.ContainsKey("rol"))
                user.Rol = result["rol"].ToString().Trim();
            if (result.ContainsKey("latitud"))
                user.Latitud = float.Parse(result["latitud"].ToString());
            if (result.ContainsKey("longitud"))
                user.Longitud = float.Parse(result["longitud"].ToString());
            if (result.ContainsKey("eventosCreados"))
                user.CantidadEventos = int.Parse(result["eventosCreados"].ToString());
            if (result.ContainsKey("IdMembresia"))
                user.CantidadEventos = int.Parse(result["IdMembresia"].ToString());
            if (result.ContainsKey("estado"))
                user.Estado = bool.Parse(result["estado"].ToString());

            if (result.ContainsKey("fechaNacimiento"))
            {
                string dateString = result["fechaNacimiento"].ToString();
                DateTime fechaNacimiento = DateTime.ParseExact(dateString, "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
                user.FechaNacimiento = fechaNacimiento;
            }

            // Otros mapeos de propiedades del objeto User

            return user;
        }

    
    }
}
