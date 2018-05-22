using Dados;
using Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ContratoNegocio : NegocioBase<ContratoEntidade>
    {
        public IEnumerable<ContratoEntidade> ListarTodosCompleto()
        {
            return new ContratoDados().ListarTodosCompleto();
        }

        public ContratoEntidade ListarCompleto(long aCodigo)
        {
            return new ContratoDados().ListarCompleto(aCodigo);
        }
    }
}
