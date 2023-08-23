using DataAccess.Crud;
using DataAccess.Dao;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic
{
    public class ValidacionCuentaManager
	{
        private ValidacionCuentaCrudFactory validacionCuentaCrudFactory;

        public ValidacionCuentaManager()
        {
            validacionCuentaCrudFactory = new ValidacionCuentaCrudFactory();
        }

        public dynamic ActualizarEstadoOTPUsuario(int idUsuario) {

            return validacionCuentaCrudFactory.ActualizarEstadoOTPUsuario(idUsuario);
      
        }


    }
}
