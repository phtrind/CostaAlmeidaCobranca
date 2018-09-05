using Dapper.FluentMap.Dommel.Mapping;
using Entidade;

namespace Dados.Mapeamento
{
    public class EnderecoMap : DommelEntityMap<EnderecoEntidade>
    {
        public EnderecoMap()
        {
            ToTable("END_ENDERECOS");

            Map(x => x.Id).ToColumn("END_CODIGO").IsKey().IsIdentity();
            Map(x => x.Logradouro).ToColumn("END_LOGRADOURO");
            Map(x => x.Numero).ToColumn("END_NUMERO");
            Map(x => x.Complemento).ToColumn("END_COMPLEMENTO");
            Map(x => x.Bairro).ToColumn("END_BAIRRO");
            Map(x => x.Cep).ToColumn("END_CEP");
            Map(x => x.Cidade).ToColumn("END_CIDADE");
            Map(x => x.Estado).ToColumn("END_ESTADO");
            Map(x => x.DataCadastro).ToColumn("END_DTHCADASTRO");
            Map(x => x.DataAlteracao).ToColumn("END_DTHALTERACAO");
            Map(x => x.IdUsuarioAlteracao).ToColumn("USU_CODIGO");

            Map(x => x.UsuarioAlteracao).Ignore();
        }
    }
}
