using Entidade;
using System.Collections.Generic;

namespace Dados
{
    public interface IRepository<T> where T : EntidadeBase
    {
        IEnumerable<T> ListarTodos();

        T Listar(long aCodigo);

        long Inserir(T aObjeto);

        bool Excluir(T aObjeto);

        bool Atualizar(T aObjeto);
    }
}
