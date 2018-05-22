using Dados;
using Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class EventoNegocio : NegocioBase<EventoEntidade>
    {
        public override IEnumerable<EventoEntidade> ListarTodos()
        {
            return new EventoDados().ListarTodos();
        }

        public override EventoEntidade Listar(long aCodigo)
        {
            return new EventoDados().Listar(aCodigo);
        }
    }
}
