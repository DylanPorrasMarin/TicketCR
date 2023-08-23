using DataAccess.Dao;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mapper
{
    public class RestablecerClaveMapper : ICrudStatements, IObjectMapper
    {
   
        public dynamic UpdatePasswordByEmail(string emailAddress, string newPassword)
        {
            var operation = new SqlOperation()
            {
                ProcedureName = "ActualizarClavePorCorreo"
            };
            operation.AddVarcharParam("Correo", emailAddress);
            operation.AddVarcharParam("NuevaClave", newPassword);

            return operation;
        }

        public T MapObjectFromResult<T>(Dictionary<string, object> result)
        {
            // Aquí debes implementar la lógica para mapear los datos del resultado al objeto T
            // Puedes usar técnicas como reflection, AutoMapper u otros métodos de mapeo
            // Ejemplo básico: si T es una interfaz IUser, crea una implementación con los datos del resultado
            if (typeof(T) == typeof(Usuario))
            {
                Usuario user = new Usuario();
                if (result.ContainsKey("correo"))
                    user.Correo = result["correo"].ToString();

                if (result.ContainsKey("clave"))
                    user.Clave = result["clave"].ToString();

                return (T)(object)user;
            }

            // Otros casos de mapeo específicos para diferentes tipos

            // Si no se puede mapear al tipo T, devuelve null o lanza una excepción
            return default(T);
        }

        public Usuario MapObjectFromResult(Dictionary<string, object> result)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetRetrieveByEmailStatement(string correo)
        {
            throw new NotImplementedException();
        }
    }
}
