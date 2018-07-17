using Entidade;
using Negocio;
using Projecao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CostaAlmeidaCobranca.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ClienteController : ApiController
    {
        [Authorize]
        // GET: api/Cliente
        public IEnumerable<ClienteEntidade> Get()
        {
            //return new ClienteNegocio().ListarTodosCompleto();

            return new ClienteNegocio().ListarTodos().OrderBy(x => x.Nome);
        }

        [Authorize]
        // GET: api/Cliente/5
        public ClienteEntidade Get(int id)
        {
            return new ClienteNegocio().ListarCompleto(id);
        }

        [Authorize]
        // POST: api/Cliente
        public long Post([FromBody]ClienteEntidade aEntidade)
        {
            aEntidade.DataCadastro = DateTime.Now;

            aEntidade.Endereco.DataCadastro = DateTime.Now;

            aEntidade.IdEndereco = new EnderecoNegocio().Inserir(aEntidade.Endereco);

            return new ClienteNegocio().Inserir(aEntidade);
        }

        [Authorize]
        // PUT: api/Cliente/5
        public bool Put([FromBody]ClienteEntidade aEntidade)
        {
            return new ClienteNegocio().Atualizar(aEntidade);
        }

        [Authorize]
        // DELETE: api/Cliente/5
        public bool Delete(int id)
        {
            var negocio = new ClienteNegocio();
            var entidade = negocio.Listar(id);

            return negocio.Excluir(entidade);
        }
    }
}
