using Dapper.FluentMap.Dommel.Mapping;
using Entidade;

namespace Dados.Mapeamento
{
    public class EventoMap : DommelEntityMap<EventoEntidade>
    {
        public EventoMap()
        {
            ToTable("EVE_EVENTOS");

            Map(x => x.Id).ToColumn("EVE_CODIGO").IsKey().IsIdentity();

            Map(x => x.Nome).ToColumn("EVE_NOME");
            Map(x => x.Data).ToColumn("EVE_DATA");

            Map(x => x.DataCadastro).ToColumn("EVE_DTHCADASTRO");
            Map(x => x.DataAlteracao).ToColumn("EVE_DTHALTERACAO");

            Map(x => x.IdEndereco).ToColumn("END_CODIGO");
            Map(x => x.IdUsuarioCadastro).ToColumn("USU_CADASTRO");
            Map(x => x.IdUsuarioAlteracao).ToColumn("USU_ALTERACAO");

            Map(x => x.Endereco).Ignore();
            Map(x => x.UsuarioCadastro).Ignore();
            Map(x => x.UsuarioAlteracao).Ignore();

            Map(x => x.Contratos).Ignore();
        }
    }
}
