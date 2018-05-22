using Entidade;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CostaAlmeidaCobranca.Controllers
{
    public class FinanceiroController : ApiController
    {
        // GET: api/Financeiro
        public IEnumerable<FinanceiroEntidade> Get()
        {
            return new FinanceiroNegocio().ListarTodos();
        }

        // GET: api/Financeiro/5
        public FinanceiroEntidade Get(int id)
        {
            return new FinanceiroNegocio().Listar(id);
        }

        // POST: api/Financeiro
        public long Post([FromBody]FinanceiroEntidade aEntidade)
        {
            aEntidade.DataCadastro = DateTime.Now;

            return new FinanceiroNegocio().Inserir(aEntidade);
        }

        // PUT: api/Financeiro/5
        public bool Put(int id, [FromBody]FinanceiroEntidade aEntidade)
        {
            return new FinanceiroNegocio().Atualizar(aEntidade);
        }

        // DELETE: api/Financeiro/5
        public bool Delete(int id)
        {
            var negocio = new FinanceiroNegocio();
            var entidade = negocio.Listar(id);

            return negocio.Excluir(entidade);
        }
    }
}
