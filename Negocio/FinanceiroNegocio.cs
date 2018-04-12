using Dados;
using Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class FinanceiroNegocio : NegocioBase<FinanceiroEntidade>
    {
        public override IEnumerable<FinanceiroEntidade> ListarTodos()
        {
            return new FinanceiroDados().ListarTodos();
        }
    }
}
