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
    public class ClienteController : ApiController
    {
        // GET: api/Cliente
        public IEnumerable<ClienteEntidade> Get()
        {
            return new ClienteNegocio().ListarTodos();
        }

        // GET: api/Cliente/5
        public ClienteEntidade Get(int id)
        {
            return new ClienteNegocio().Listar(id);
        }

        // POST: api/Cliente
        public long Post([FromBody]ClienteEntidade aEntidade)
        {
            aEntidade.DataCadastro = DateTime.Now;

            return new ClienteNegocio().Inserir(aEntidade);
        }

        // PUT: api/Cliente/5
        public bool Put([FromBody]ClienteEntidade aEntidade)
        {
            return new ClienteNegocio().Atualizar(aEntidade);
        }

        // DELETE: api/Cliente/5
        public bool Delete(int id)
        {
            var negocio = new ClienteNegocio();
            var entidade = negocio.Listar(id);

            return negocio.Excluir(entidade);
        }
    }
}
