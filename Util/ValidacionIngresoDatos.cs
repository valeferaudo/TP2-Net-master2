using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

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
        {
            string expresion = "^(([\\w-]+\\.)+[\\w-]+|([a-zA-Z]{1}|[\\w-]{2,}))@(([a-zA-Z]+[\\w-]+\\.){1,2}[a-zA-Z]{2,4})$";
            if (Regex.IsMatch(email,expresion))
                {
                return true;
                
                }
            else
            {
                return false;
            }
        }
    }
    
}
