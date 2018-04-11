using Dados;
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
            var dados = new UsuarioDados();

            if (dados.BuscarLogin(aEmail, aSenha).Count() > 0)
                return true;

            return false;
        }
    }
}
