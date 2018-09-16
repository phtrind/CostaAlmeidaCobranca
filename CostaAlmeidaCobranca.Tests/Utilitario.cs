using Enumerador;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utilitario;

namespace CostaAlmeidaCobranca.Tests
{
    [TestClass]
    public class Utilitario
    {
        [TestMethod]
        public void GetEnumDescription()
        {
            string description = StringUtilitario.TraduzirEnum(StatusContratoEnum.Ativo);

            Assert.IsTrue("Ativo" == description);
        }
    }
}
