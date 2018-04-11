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
    public class UsuarioController : ApiController
    {
        // GET: api/Usuario
        public IEnumerable<UsuarioEntidade> Get()
        {
            return new UsuarioNegocio().ListarTodos();
        }

        // GET: api/Usuario/5
        public UsuarioEntidade Get(int aId)
        {
            return new UsuarioNegocio().Listar(aId);
        }

        // POST: api/Usuario
        public long Post([FromBody]UsuarioEntidade aUsuario)
        {
            aUsuario.DataCadastro = DateTime.Now;

            return new UsuarioNegocio().Inserir(aUsuario);
        }

        // PUT: api/Usuario/5
        public bool Put([FromBody]UsuarioEntidade aUsuario)
        {
            return new UsuarioNegocio().Atualizar(aUsuario);
        }

        // DELETE: api/Usuario/5
        public bool Delete(int id)
        {
            var negocio = new UsuarioNegocio();
            var entidade = negocio.Listar(id);

            return negocio.Excluir(entidade);
        }
    }
}
