using Dapper.FluentMap.Dommel.Mapping;
using Entidade;

namespace Dados.Mapeamento
{
    public class FuncionarioMap : DommelEntityMap<FuncionarioEntidade>
    {
        public FuncionarioMap()
        {
            ToTable("FUN_FUNCIONARIOS");

            Map(x => x.Id).ToColumn("FUN_CODIGO").IsKey().IsIdentity();
            Map(x => x.Nome).ToColumn("FUN_NOME");
            Map(x => x.Permissao).ToColumn("FUN_PERMISSAO");
            Map(x => x.Cpf).ToColumn("FUN_CPF");
            Map(x => x.TelefoneFixo).ToColumn("FUN_TELFIXO");
            Map(x => x.TelefoneCelular).ToColumn("FUN_TELCELULAR");

            Map(x => x.IdUsuario).ToColumn("USU_CODIGO");

            Map(x => x.DataCadastro).ToColumn("FUN_DTHCADASTRO");

            Map(x => x.Usuario).Ignore();
        }
    }
}
