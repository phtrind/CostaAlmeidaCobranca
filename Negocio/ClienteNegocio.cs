using Dados;
using Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ClienteNegocio : NegocioBase<ClienteEntidade>
    {
        public override IEnumerable<ClienteEntidade> ListarTodos()
        {
            return new ClienteDados().ListarTodos();
        }

        public override ClienteEntidade Listar(long aCodigo)
        {
            return new ClienteDados().Listar(aCodigo);
        }
    }
}
