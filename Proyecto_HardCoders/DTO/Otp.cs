using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Otp
    {
        public Otp() { }

        public int idOtp { get; set; }
        public int idUsuario { get; set; }
        public int otp { get; set; }
        public bool estado { get; set; }

    }
}
