using Dados;
using Entidade;
using System.Collections.Generic;

namespace Negocio
{
    public class NegocioBase<T> : INegocioBase<T>  where T : EntidadeBase
    {

        public bool Atualizar(T aObjeto)
        {
            return new DadosBase<T>().Atualizar(aObjeto);
        }

        public bool Excluir(T aObjeto)
        {
            return new DadosBase<T>().Excluir(aObjeto);
        }

        public long Inserir(T aObjeto)
        {
            return new DadosBase<T>().Inserir(aObjeto);
        }

        public T Listar(long aCodigo)
        {
            return new DadosBase<T>().Listar(aCodigo);
        }

        public IEnumerable<T> ListarTodos()
        {
            return new DadosBase<T>().ListarTodos();
        }
    }
}
