using Dados.Mapeamento;
using Dapper.FluentMap;
using Dapper.FluentMap.Dommel;

namespace Dados
{
    public static class RegisterMappings
    {
        public static void Register()
        {
            FluentMapper.Initialize(config =>
            {
                config.AddMap(new UsuarioMap());
                config.AddMap(new EnderecoMap());
                config.AddMap(new FinanceiroMap());
                config.AddMap(new ClienteMap());
                config.AddMap(new EventoMap());
                config.AddMap(new ContratoMap());
                config.AddMap(new ParcelaMap());
                config.AddMap(new FuncionarioMap());
                config.AddMap(new CrmMap());
                config.ForDommel();
            });
        }
    }
}
