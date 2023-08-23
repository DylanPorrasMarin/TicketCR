using DataAccess.Dao;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Mapper
{
    public class UsuarioMapper : ICrudStatements, IObjectMapper
    {
        public Usuario BuildObject(Dictionary<string, object> objectRow)
        {
            var usuario = new Usuario();

            if (objectRow.ContainsKey("idUsuario"))
                usuario.IdUsuario = int.Parse(objectRow["idUsuario"].ToString());
            if (objectRow.ContainsKey("clave"))
                usuario.Clave = objectRow["clave"].ToString();
            if (objectRow.ContainsKey("nombre"))
                usuario.Nombre = objectRow["nombre"].ToString();
            if (objectRow.ContainsKey("apellidos"))
                usuario.Apellidos = objectRow["apellidos"].ToString();
            if (objectRow.ContainsKey("cedula"))
                usuario.Cedula = objectRow["cedula"].ToString();
            if (objectRow.ContainsKey("correo"))
                usuario.Correo = objectRow["correo"].ToString();
            if (objectRow.ContainsKey("telefono"))
                usuario.Telefono = objectRow["telefono"].ToString();
            if (objectRow.ContainsKey("fechaNacimiento"))
                usuario.FechaNacimiento = DateTime.Parse(objectRow["fechaNacimiento"].ToString());
            if (objectRow.ContainsKey("latitud"))
                usuario.Latitud = float.Parse(objectRow["latitud"].ToString());
            if (objectRow.ContainsKey("longitud"))
                usuario.Longitud = float.Parse(objectRow["longitud"].ToString());
            if (objectRow.ContainsKey("rol"))
                usuario.Rol = objectRow["rol"].ToString();
            if (objectRow.ContainsKey("eventosCreados"))
                usuario.CantidadEventos = int.Parse(objectRow["eventosCreados"].ToString());
            if (objectRow.ContainsKey("idMembresia"))
                usuario.IdMembresia = int.Parse(objectRow["idMembresia"].ToString());
            if (objectRow.ContainsKey("estado"))
                usuario.Estado = bool.Parse(objectRow["estado"].ToString());

            return usuario;
        }

        public List<Usuario> BuildObjects(List<Dictionary<string, object>> rows)
        {
            var usuarios = new List<Usuario>();

            foreach (var row in rows)
            {
                usuarios.Add(BuildObject(row));
            }

            return usuarios;
        }

        public SqlOperation InsertarUsuario(Usuario usuario)
        {
            var operation = new SqlOperation()
            {
                ProcedureName = "InsertarUsuario"
            };

         
            operation.AddVarcharParam("Nombre", usuario.Nombre);
            operation.AddVarcharParam("Apellidos", usuario.Apellidos);
            operation.AddVarcharParam("Cedula", usuario.Cedula);
            operation.AddVarcharParam("Correo", usuario.Correo);
            operation.AddVarcharParam("Telefono", usuario.Telefono);
            operation.AddVarcharParam("Clave", usuario.Clave);
            operation.AddVarcharParam("Rol", usuario.Rol);
            operation.AddFloatParam("Latitud", usuario.Latitud);
            operation.AddFloatParam("Longitud", usuario.Longitud);
            operation.AddDateTimeParam("FechaNacimiento", usuario.FechaNacimiento);



            // Parámetro de salida para el idUsuario generado
            operation.AddOutputParam("idUsuario", SqlDbType.Int);

            return operation;
        }

        public SqlOperation InsertarUsuarioDesdeAdmin(Usuario usuario)
        {
            var operation = new SqlOperation()
            {
                ProcedureName = "InsertarUsuarioDesdeAdmin"
            };


            operation.AddVarcharParam("Nombre", usuario.Nombre);
            operation.AddVarcharParam("Apellidos", usuario.Apellidos);
            operation.AddVarcharParam("Cedula", usuario.Cedula);
            operation.AddVarcharParam("Correo", usuario.Correo);
            operation.AddVarcharParam("Telefono", usuario.Telefono);
            operation.AddVarcharParam("Clave", usuario.Clave);
            operation.AddVarcharParam("Rol", usuario.Rol);
            operation.AddFloatParam("Latitud", usuario.Latitud);
            operation.AddFloatParam("Longitud", usuario.Longitud);
            operation.AddDateTimeParam("FechaNacimiento", usuario.FechaNacimiento);


            // Parámetro de salida para el idUsuario generado
            operation.AddOutputParam("idUsuario", SqlDbType.Int);

            return operation;
        }



        public SqlOperation ActualizarUsuario(Usuario usuario)
        {
            var operation = new SqlOperation()
            {
                ProcedureName = "ActualizarUsuario"
            };

            operation.AddIntegerParam("IdUsuario", usuario.IdUsuario);
            operation.AddVarcharParam("Nombre", usuario.Nombre);
            operation.AddVarcharParam("Apellidos", usuario.Apellidos);
            operation.AddVarcharParam("Cedula", usuario.Cedula);
            operation.AddVarcharParam("Correo", usuario.Correo);
            operation.AddVarcharParam("Telefono", usuario.Telefono);
            operation.AddVarcharParam("Clave", usuario.Clave);
            operation.AddVarcharParam("Rol", usuario.Rol);
            operation.AddFloatParam("Latitud", usuario.Latitud);
            operation.AddFloatParam("Longitud", usuario.Longitud);
            operation.AddDateTimeParam("FechaNacimiento", usuario.FechaNacimiento);

     
     

            //operation.AddOutputParam("idUsuario", SqlDbType.Int);

            return operation;
        }
        public SqlOperation ActualizarEstadoUsuario(int idUsuario, bool estado)
        {
            var operation = new SqlOperation()
            {
                ProcedureName = "ActualizarEstadoUsuario"
            };

            operation.AddIntegerParam("idUsuario", idUsuario);
            operation.AddBitParam("nuevoEstado", estado);
           ;

            //operation.AddOutputParam("idUsuario", SqlDbType.Int);

            return operation;
        }

        public SqlOperation ObtenerUsuarioPorId(int idUsuario)
        {
            var operation = new SqlOperation()
            {
                ProcedureName = "ObtenerUsuarioPorId"
            };
            //operation.AddOutputParam("idUsuario", SqlDbType.Int);
            operation.AddIntegerParam("IdUsuario", idUsuario);
           
            return operation;
        }


        public SqlOperation ObtenerUsuarios()
        {
            return new SqlOperation()
            {
                ProcedureName = "ObtenerUsuariosRegistrados"
            };
        }

        public SqlOperation VerificarCorreoIdentico(string correo)
        {
            var operation = new SqlOperation()
            {
                ProcedureName = "VerificarCorreoExistente"
            };

            operation.AddVarcharParam("Correo", correo);

            return operation;
        }



        public SqlOperation GetRetrieveByEmailStatement(string correo)
        {
            var operation = new SqlOperation()
            {
                ProcedureName = "ObtenerUsuarioPorCorreo"
            };

            operation.AddVarcharParam("Correo", correo);

            return operation;
        }

        public Usuario MapObjectFromResult(Dictionary<string, object> result)
        {
            return BuildObject(result);
        }
    }
}
