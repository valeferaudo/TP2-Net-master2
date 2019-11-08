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
            string expresion = "^([0-9])*$";
            if(Regex.IsMatch(strnro, expresion))
            {
                return true;
            }
            else
            {
                return false;
            }


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
