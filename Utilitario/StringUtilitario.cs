using System;
using System.Globalization;
using System.Linq;

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

        public static string TratarDataTime(dynamic aDateTime)
        {
            if (aDateTime == null)
            {
                return string.Empty;
            }

            try
            {
                DateTime data = Convert.ToDateTime(aDateTime);

                return data.ToString("dd/MM/yyyy");
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static string TratarStringNula(string aString)
        {
            if (string.IsNullOrEmpty(aString))
            {
                return string.Empty;
            }
            else
            {
                return aString;
            }
        }

        public static string TraduzirEnum(Enum aEnum)
        {
            return Enum.GetName(aEnum.GetType(), aEnum);
        }
    }
}
