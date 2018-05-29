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
    public class EventoController : ApiController
    {
        [Authorize]
        // GET: api/Evento
        public IEnumerable<EventoEntidade> Get()
        {
            return new EventoNegocio().ListarTodos();
        }

        [Authorize]
        // GET: api/Evento/5
        public EventoEntidade Get(int id)
        {
            return new EventoNegocio().Listar(id);
        }

        [Authorize]
        // POST: api/Evento
        public long Post([FromBody]EventoEntidade aEntidade)
        {
            aEntidade.DataCadastro = DateTime.Now;

            return new EventoNegocio().Inserir(aEntidade);
        }

        [Authorize]
        // PUT: api/Evento/5
        public bool Put([FromBody]EventoEntidade aEntidade)
        {
            return new EventoNegocio().Atualizar(aEntidade);
        }

        [Authorize]
        // DELETE: api/Evento/5
        public bool Delete(int id)
        {
            var negocio = new EventoNegocio();
            var entidade = negocio.Listar(id);

            return negocio.Excluir(entidade);
        }
    }
}
