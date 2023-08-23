using DataAccess.Crud;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic
{
    public class CompraBoletosManager
    {

        private CompraBoletosCrudFactory compraBoletosCrudFactory;

        public CompraBoletosManager()
        {
            compraBoletosCrudFactory = new CompraBoletosCrudFactory();
        }


        public dynamic InsertarCompraBoletosVirtuales(CompraBoletosVirtuales compraBoletosVirtuales)
        {
            return compraBoletosCrudFactory.InsertarCompraBoletosVirtuales(compraBoletosVirtuales);

        }

        public dynamic InsertarCompraBoletosPresenciales(CompraBoletosPresenciales compraBoletosPresenciales)
        {
            return compraBoletosCrudFactory.InsertarCompraBoletosPresenciales(compraBoletosPresenciales);

        }




    }
}
