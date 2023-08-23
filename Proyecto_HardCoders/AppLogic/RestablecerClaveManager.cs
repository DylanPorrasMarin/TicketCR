using DataAccess.Crud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic
{
    public class RestablecerClaveManager
    {

        public void UpdatePasswordByEmail(string emailAddress, string newPassword)
        {
            RestablecerClaveCrudFactory restablecerClaveCrudFactory = new RestablecerClaveCrudFactory();

            restablecerClaveCrudFactory.UpdatePasswordByEmail(emailAddress, newPassword); 
        }
    }
}
