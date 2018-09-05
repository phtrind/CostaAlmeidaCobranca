using Dapper.FluentMap.Dommel.Mapping;
using Entidade;

namespace Dados.Mapeamento
{
    public class ContratoMap : DommelEntityMap<ContratoEntidade>
    {
        public ContratoMap()
        {
            ToTable("CON_CONTRATOS");

            Map(x => x.Id).ToColumn("CON_CODIGO").IsKey().IsIdentity();
            Map(x => x.Valor).ToColumn("CON_VALOR");
            Map(x => x.Animal).ToColumn("CON_ANIMAL");
            Map(x => x.Observacao).ToColumn("CON_OBSERVACAO");
            Map(x => x.Status).ToColumn("CON_STATUS");

            Map(x => x.DataCadastro).ToColumn("CON_DTHCADASTRO");
            Map(x => x.DataAlteracao).ToColumn("CON_DTHALTERACAO");

            Map(x => x.IdVendedor).ToColumn("CON_VENDEDOR");
            Map(x => x.IdComprador).ToColumn("CON_COMPRADOR");
            Map(x => x.IdEvento).ToColumn("EVE_CODIGO");
            Map(x => x.IdUsuario).ToColumn("USU_CODIGO");

            Map(x => x.Usuario).Ignore();
            Map(x => x.Evento).Ignore();
            Map(x => x.Vendedor).Ignore();
            Map(x => x.Comprador).Ignore();
            Map(x => x.Parcelas).Ignore();
        }
    }
}
