using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mapper
{
    public interface IObjectMapper
    {
        //Crear objeto de informacion que venga de la BD
        public Usuario MapObjectFromResult(Dictionary<string, object> result);
    }
}
