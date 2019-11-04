using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Util
{
    public class ValidacionIngresoDatos
    {
        public static bool EsNumero(String strnro)
        {
            int nro = 0;
            bool result;
            try
            {
                nro = Int32.Parse(strnro);
                result = true;
            }
            catch (FormatException)
            {
                result = false;
            }
            return result;
        }
        public static bool EsMail(string email)
        { //hacerlo con expresion regular
            try
            {
                MailAddress m = new MailAddress(email);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
    
}
