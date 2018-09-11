using Dados;
using Entidade;
using System.Collections.Generic;

namespace Negocio
{
    public abstract class NegocioBase<T> : INegocioBase<T>  where T : EntidadeBase
    {

        public virtual bool Atualizar(T aObjeto)
        {
            this.ValidateRegister(aObjeto);

            return new DadosBase<T>().Atualizar(aObjeto);
        }

        public virtual bool Excluir(T aObjeto)
        {
            return new DadosBase<T>().Excluir(aObjeto);
        }

        public virtual long Inserir(T aObjeto)
        {
            this.ValidateRegister(aObjeto);

            return new DadosBase<T>().Inserir(aObjeto);
        }

        public virtual T Listar(long aCodigo)
        {
            return new DadosBase<T>().Listar(aCodigo);
        }

        public virtual IEnumerable<T> ListarTodos()
        {
            return new DadosBase<T>().ListarTodos();
        }

        public abstract void ValidateRegister(T aEntidade);
    }
}
