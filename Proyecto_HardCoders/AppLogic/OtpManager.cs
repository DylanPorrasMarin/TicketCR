using DataAccess.Crud;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic
{
    public class OtpManager
    {
		private OtpCrudFactory otpCrudFactory;

		public OtpManager()
		{
			otpCrudFactory = new OtpCrudFactory();
		}
		public void CreateOtp(Otp otp)
        {
            OtpCrudFactory ocrud = new OtpCrudFactory();
            ocrud.CreateOtp(otp);
        }
    }
}
