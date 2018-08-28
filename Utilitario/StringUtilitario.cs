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

        public static string GerarSenhaAlatoria()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, 6)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }
    }
}
