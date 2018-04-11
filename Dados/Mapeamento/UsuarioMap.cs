using Dapper.FluentMap.Dommel.Mapping;
using Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados.Mapeamento
{
    public class UsuarioMap : DommelEntityMap<UsuarioEntidade>
    {
        public UsuarioMap()
        {
            ToTable("USUARIOS");

            Map(x => x.Id).ToColumn("COD_CODUSUARIO").IsKey();
            Map(x => x.Email).ToColumn("TXT_EMAIL").IsKey();
            Map(x => x.Senha).ToColumn("TXT_SENHA");
            Map(x => x.Tipo).ToColumn("IDC_TIPOUSUARIO");
            Map(x => x.DataCadastro).ToColumn("DTH_DATACADASTRO");
        }
    }
}
