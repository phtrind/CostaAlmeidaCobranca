using Dados;
using Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ParcelaNegocio : NegocioBase<ParcelasEntidade>
    {
        public IEnumerable<ParcelasEntidade> ListarTodosCompleto()
        {
            return new ParcelaDados().ListarTodosCompleto();
        }

        public ParcelasEntidade ListarCompleto(long aCodigo)
        {
            return new ParcelaDados().ListarCompleto(aCodigo);
        }

        public IEnumerable<ParcelasEntidade> ParcelasPorContrato(int id)
        {
            return new ParcelaDados().ParcelasPorContrato(id);
        }
    }
}
