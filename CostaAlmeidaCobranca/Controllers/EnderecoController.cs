using Entidade;
using Negocio;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CostaAlmeidaCobranca.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class EnderecoController : ApiController
    {
        [Authorize]
        // GET: api/Endereco
        public IEnumerable<EnderecoEntidade> Get()
        {
            return new EnderecoNegocio().ListarTodos();
        }

        [Authorize]
        // GET: api/Endereco/5
        public EnderecoEntidade Get(int aId)
        {
            return new EnderecoNegocio().Listar(aId);
        }

        [Authorize]
        // POST: api/Endereco
        public long Post([FromBody]EnderecoEntidade aEntidade)
        {
            aEntidade.DataCadastro = DateTime.Now;

            return new EnderecoNegocio().Inserir(aEntidade);
        }

        [Authorize]
        // PUT: api/Endereco/5
        public bool Put([FromBody]EnderecoEntidade aEntidade)
        {
            return new EnderecoNegocio().Atualizar(aEntidade);
        }

        [Authorize]
        // DELETE: api/Endereco/5
        public bool Delete(int id)
        {
            var negocio = new EnderecoNegocio();
            var entidade = negocio.Listar(id);

            return negocio.Excluir(entidade);
        }
    }
}
