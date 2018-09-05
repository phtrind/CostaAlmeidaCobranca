using Dapper.FluentMap.Dommel.Mapping;
using Entidade;

namespace Dados.Mapeamento
{
    public class FinanceiroMap : DommelEntityMap<FinanceiroEntidade>
    {
        public FinanceiroMap()
        {
            ToTable("TRA_TRANSACOES");

            Map(x => x.Id).ToColumn("TRA_CODIGO").IsKey().IsIdentity();
            Map(x => x.Valor).ToColumn("TRA_VALOR");
            Map(x => x.Tipo).ToColumn("TRA_TIPO");
            Map(x => x.Data).ToColumn("TRA_DATA");
            Map(x => x.Status).ToColumn("TRA_STATUS");

            Map(x => x.DataCadastro).ToColumn("TRA_DTHCADASTRO");
            Map(x => x.DataAlteracao).ToColumn("TRA_DTHALTERACAO");

            Map(x => x.IdUsuario).ToColumn("USU_CODIGO");

            Map(x => x.Usuario).Ignore();
        }
    }
}
