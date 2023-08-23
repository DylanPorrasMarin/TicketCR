using DataAccess.Dao;
using DataAccess.Mapper;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Crud
{
    public class OtpCrudFactory : CrudFactory
    {
        private OtpMapper mapper;

		public OtpCrudFactory() : base()
		{
			mapper = new OtpMapper();
			dao = SqlDao.GetInstance();
		}
		
		

        public void CreateOtp(Otp otp)
        {
            SqlOperation operation = mapper.InsertarOtp(otp);
            dao.ExecuteStoredProcedure(operation);
        }

        public Otp ObtenerOTPporIDUsuario(int idUsuario)
        {
            SqlOperation operation = mapper.ObtenerOTPporIDUsuario(idUsuario);
            var result = dao.ExecuteStoredProcedureWithQuery(operation);

            if (result.Count > 0)
            {
                return mapper.BuildObject(result[0]);
            }

            return null; // Si no se encuentra el usuario, se devuelve null
        }
        public override void Create()
        {
            throw new NotImplementedException();
        }

        public override void Delete()
        {
            throw new NotImplementedException();
        }

        public override List<T> RetrieveAll<T>()
        {
            throw new NotImplementedException();
        }

        public override T RetrieveByEmail<T>(string correo)
        {
            throw new NotImplementedException();
        }

        public override T RetrieveById<T>(int id)
        {
            throw new NotImplementedException();
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }

        public static implicit operator OtpCrudFactory(OtpMapper v)
        {
            throw new NotImplementedException();
        }
    }
}
