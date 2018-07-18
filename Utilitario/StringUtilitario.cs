using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilitario
{
    public static class StringUtilitario
    {
        public static string ValorReais(decimal aValor)
        {
            return string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", aValor);
        }
    }
}
