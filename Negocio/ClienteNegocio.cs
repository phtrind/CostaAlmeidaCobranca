using Dados;
using Entidade;
using Projecao;
using System.Collections.Generic;

namespace Negocio
{
    public class ClienteNegocio : NegocioBase<ClienteEntidade>
    {
        public IEnumerable<ComboProjecao> getComboClientes()
        {
            return new ClienteDados().getComboClientes();
        }
    }
}
