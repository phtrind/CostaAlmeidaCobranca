using Entidade;
using Negocio;
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
    public class UsuarioController : ApiController
    {
        [Authorize]
        // GET: api/Usuario
        public IEnumerable<UsuarioEntidade> Get()
        {
            return new UsuarioNegocio().ListarTodos();
        }

        [Authorize]
        // GET: api/Usuario/5
        public UsuarioEntidade Get(int aId)
        {
            return new UsuarioNegocio().Listar(aId);
        }

        [Authorize]
        [Route("api/Usuario/Username/{aEmail}")]
        [HttpGet]
        public UsuarioEntidade getUserByEmail(string aEmail)
        {
            return new UsuarioNegocio().ListarTodos().Where(x => x.Email == aEmail).FirstOrDefault();
        }

        [Authorize]
        // POST: api/Usuario
        public long Post([FromBody]UsuarioEntidade aEntidade)
        {
            aEntidade.DataCadastro = DateTime.Now;

            return new UsuarioNegocio().Inserir(aEntidade);
        }

        [Authorize]
        // PUT: api/Usuario/5
        public bool Put([FromBody]UsuarioEntidade aEntidade)
        {
            return new UsuarioNegocio().Atualizar(aEntidade);
        }

        [Authorize]
        // DELETE: api/Usuario/5
        public bool Delete(int id)
        {
            var negocio = new UsuarioNegocio();
            var entidade = negocio.Listar(id);

            return negocio.Excluir(entidade);
        }
    }
}
