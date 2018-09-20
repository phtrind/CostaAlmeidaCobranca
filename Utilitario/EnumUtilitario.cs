using Projecao;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Utilitario
{
    public static class EnumUtilitario
    {
        public static IEnumerable<ComboProjecao> ConverterParaCombo(Type aEnum)
        {
            try
            {
                return Enum.GetValues(aEnum).OfType<Enum>().AsEnumerable().Select(x => new ComboProjecao()
                {
                    Codigo = Convert.ToInt64(x),
                    Descricao = StringUtilitario.TraduzirEnum(x)
                });
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
