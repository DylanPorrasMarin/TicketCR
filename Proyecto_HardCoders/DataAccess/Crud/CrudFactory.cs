using DataAccess.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Crud
{
    public abstract class CrudFactory
    {

        protected SqlDao dao;

        public abstract void Create();
        public abstract void Update();
        public abstract void Delete();
        public abstract T RetrieveById<T>(int id);
        public abstract List<T> RetrieveAll<T>();
        public abstract T RetrieveByEmail<T>(string correo);
		
	}
}
