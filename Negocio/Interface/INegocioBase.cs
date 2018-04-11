using Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public interface INegocioBase<T> where T : EntidadeBase
    {
        IEnumerable<T> ListarTodos();

        T Listar(long aCodigo);

        long Inserir(T aObjeto);

        bool Excluir(T aObjeto);

        bool Atualizar(T aObjeto);
    }
}
