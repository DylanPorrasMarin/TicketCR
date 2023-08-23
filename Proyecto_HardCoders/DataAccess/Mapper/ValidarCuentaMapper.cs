using DataAccess.Dao;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mapper
{
	public class ValidarCuentaMapper : ICrudStatements, IObjectMapper
	{
		public SqlOperation GetRetrieveByEmailStatement(string correo)
		{
			throw new NotImplementedException();
		}

		public Usuario MapObjectFromResult(Dictionary<string, object> result)
		{
			throw new NotImplementedException();
		}


		public SqlOperation ActualizarEstadoOTPUsuario(int idUsuario)
		{

			var operation = new SqlOperation()
			{
				ProcedureName = "ActualizarEstadoOTPUsuario"
            };
			operation.AddIntegerParam("idUsuario", idUsuario);

			return operation;

		}
        
    }
}
