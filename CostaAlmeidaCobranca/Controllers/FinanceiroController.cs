using Entidade;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace CostaAlmeidaCobranca.Controllers
{
    public class FinanceiroController : ApiController
    {
        [Authorize]
        // GET: api/Financeiro
        public IEnumerable<FinanceiroEntidade> Get()
        {
            return new FinanceiroNegocio().ListarTodos();
        }

        [Authorize]
        // GET: api/Financeiro/5
        public FinanceiroEntidade Get(int id)
        {
            return new FinanceiroNegocio().Listar(id);
        }

        [Authorize]
        // POST: api/Financeiro
        public long Post([FromBody]FinanceiroEntidade aEntidade)
        {
            aEntidade.DataCadastro = DateTime.Now;

            return new FinanceiroNegocio().Inserir(aEntidade);
        }

        [Authorize]
        // PUT: api/Financeiro/5
        public bool Put(int id, [FromBody]FinanceiroEntidade aEntidade)
        {
            return new FinanceiroNegocio().Atualizar(aEntidade);
        }

        [Authorize]
        // DELETE: api/Financeiro/5
        public bool Delete(int id)
        {
            var negocio = new FinanceiroNegocio();
            var entidade = negocio.Listar(id);

            return negocio.Excluir(entidade);
        }
    }
}
