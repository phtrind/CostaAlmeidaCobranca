using Dommel;
using Entidade;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Dados
{
    public class DadosBase<T> : IRepository<T> where T : EntidadeBase
    {
        protected readonly IDbConnection db;

        public DadosBase()
        {
            db = new SqlConnection(ConfigurationManager.ConnectionStrings["costa_almeida_cobranca"].ConnectionString);
        }

        public virtual T Listar(long aCodigo)
        {
            return db.Get<T>(aCodigo);
        }

        public virtual IEnumerable<T> ListarTodos()
        {
            return db.GetAll<T>().ToList();
        }

        public virtual long Inserir(T aObjeto)
        {
            return Convert.ToInt32(db.Insert(aObjeto));
        }

        public virtual bool Atualizar(T aObjeto)
        {
            return db.Update<T>(aObjeto);
        }

        public virtual bool Excluir(T aObjeto)
        {
            return db.Delete<T>(aObjeto);
        }
    }
}
