using DataAccess.Dao;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace DataAccess.Mapper
{
    public class OtpMapper : ICrudStatements, IObjectMapper
    {
        public Otp BuildObject(Dictionary<string, object> objectRow)
        {
            var otp = new Otp();

            if (objectRow.ContainsKey("idUsuario"))
                otp.idUsuario = int.Parse(objectRow["idUsuario"].ToString());
            if (objectRow.ContainsKey("otp"))
                otp.otp = int.Parse(objectRow["otp"].ToString());
            if (objectRow.ContainsKey("estado"))
                otp.estado = bool.Parse(objectRow["estado"].ToString());


            return otp;
        }
		public SqlOperation GetRetrieveByOtpStatement(int otp)
		{
			var operation = new SqlOperation()
			{
				ProcedureName = "ValidarOtp"
			};
			operation.AddIntegerParam("Otp", otp);
			return operation;

		}

		public List<Otp> BuildObjects(List<Dictionary<string, object>> rows)
        {
            var otps = new List<Otp>();

            foreach (var row in rows)
            {
                otps.Add(BuildObject(row));
            }

            return otps;
        }

        public SqlOperation ObtenerOTPporIDUsuario(int idUsuario)
        {
            var operation = new SqlOperation()
            {
                ProcedureName = "ObtenerOTPporIDUsuario"
            };
            operation.AddIntegerParam("idUsuario", idUsuario);
            return operation;
        }

        public SqlOperation InsertarOtp(Otp otp)
        {
            var operation = new SqlOperation()
            {
                ProcedureName = "InsertarOtp"
            };


            operation.AddIntegerParam("idUsuario", otp.idUsuario);
            operation.AddIntegerParam("otp", otp.otp);
            operation.AddBooleanParam("estado", otp.estado);
            
            return operation;
        }

		public SqlOperation CambiarEstadoConOTP(Usuario usuario ,Otp otp)
		{
			var operation = new SqlOperation()
			{
				ProcedureName = "CambiarEstadoConOTP"
			};

			operation.AddIntegerParam("idUsuario", usuario.IdUsuario);
			operation.AddIntegerParam("otp", otp.otp);
			return operation;
		}

		

		public SqlOperation GetRetrieveByEmailStatement(string correo)
        {
            throw new NotImplementedException();
        }

        public Usuario MapObjectFromResult(Dictionary<string, object> result)
        {
            throw new NotImplementedException();
        }

		internal object GetRetrieveByOtpStatement<T>(int otp)
		{
			throw new NotImplementedException();
		}
	}
}
