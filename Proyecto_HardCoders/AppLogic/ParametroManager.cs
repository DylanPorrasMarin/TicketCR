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
    public class ParametroManager
    {
        private ParametroCrudFactory parametroCrudFactory;

        public ParametroManager()
        {
            parametroCrudFactory = new ParametroCrudFactory();
        }

        public void ActualizarParametro(float impuestos, float comisiones)
        {
            parametroCrudFactory.ActualizarParametro(impuestos, comisiones);
        }

   /*     public int InsertarParametro(Parametro parametro)
        {
            // Usamos la instancia de la fábrica que ya fue creada en el constructor.
            return parametroCrudFactory.InsertarParametro(parametro);
        }
*/
      /*  public List<Parametro> ObtenerParametrosRegistrados()
        {
            return parametroCrudFactory.ObtenerTodosParametros();
        }*/
    }
}
