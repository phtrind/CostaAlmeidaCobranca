using Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Dommel;

namespace Dados
{
    public class UsuarioDados : DadosBase<UsuarioEntidade>
    {
        public IEnumerable<UsuarioEntidade> BuscarLogin(string aEmail, string aSenha)
        {
            return db.Select<UsuarioEntidade>(x => x.Email == aEmail && x.Senha == aSenha);
        }
    }
}
