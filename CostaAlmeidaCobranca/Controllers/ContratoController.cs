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
    public class ContratoController : ApiController
    {
        // GET: api/Contrato
        public IEnumerable<ContratoEntidade> Get()
        {
            return new ContratoNegocio().ListarTodosCompleto();
        }

        // GET: api/Contrato/5
        public ContratoEntidade Get(int id)
        {
            return new ContratoNegocio().ListarCompleto(id);
        }

        // POST: api/Contrato
        public long Post([FromBody]ContratoEntidade aEntidade)
        {
            aEntidade.DataCadastro = DateTime.Now;

            return new ContratoNegocio().Inserir(aEntidade);
        }

        // PUT: api/Contrato/5
        public bool Put([FromBody]ContratoEntidade aEntidade)
        {
            return new ContratoNegocio().Atualizar(aEntidade);
        }

        // DELETE: api/Contrato/5
        public bool Delete(int id)
        {
            var negocio = new ContratoNegocio();
            var entidade = negocio.Listar(id);

            return negocio.Excluir(entidade);
        }
    }
}
