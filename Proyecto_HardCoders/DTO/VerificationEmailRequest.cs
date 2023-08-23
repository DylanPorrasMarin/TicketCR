using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class VerificationEmailRequest
    {
        public string EmailAddress { get; set; }
        public string VerificationCode { get; set; }
    }
}
