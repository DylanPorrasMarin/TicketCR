using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic
{


    public class EncryptionHelper
    {

        #region Encriptar clave
      
        public static string GenerateRecoveryToken()
        {
            byte[] randomBytes = new byte[32]; // 32 bytes = 256 bits (tamaño seguro para un token)
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(randomBytes);
            }
            return Convert.ToBase64String(randomBytes);
        }
        #endregion



    }
}
