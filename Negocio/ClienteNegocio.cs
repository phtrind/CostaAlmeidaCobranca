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
        public IEnumerable<ClienteEntidade> ListarTodosCompleto()
        {
            return new ClienteDados().ListarTodosCompleto();
        }

        public ClienteEntidade ListarCompleto(long aCodigo)
        {
            return new ClienteDados().ListarCompleto(aCodigo);
        }
    }
}
