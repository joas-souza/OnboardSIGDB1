using System;
using System.Collections.Generic;
using System.Text;

namespace OnboardSIGDB1.Dominio.Utils
{
    public class MetodosUteis
    {
        public static string RemoverMascara(string valor)
        {
            return valor.Replace(".", "").Replace("-", "").Replace("/", "");
        }
    }
}
