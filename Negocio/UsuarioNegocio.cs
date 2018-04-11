using Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class UsuarioNegocio : NegocioBase<UsuarioEntidade>
    {
        public bool VerificarLogin(string aEmail, string aSenha)
        {
            var teste = ListarTodos();

            if (teste.Where(x => x.Email == aEmail && x.Senha == aSenha).Count() > 0)
                return true;

            return false;
        }
    }
}
