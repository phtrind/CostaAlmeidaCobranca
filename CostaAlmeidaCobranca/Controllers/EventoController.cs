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
    public class EventoController : ApiController
    {
        [Authorize]
        // GET: api/Evento
        public IEnumerable<EventoEntidade> Get()
        {
            try
            {
                return new EventoNegocio().ListarTodos().OrderBy(x => x.Nome);
            }
            catch (Exception ex)
            {
                var erro = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(ex.Message),
                    ReasonPhrase = ex.Message
                };

                throw new HttpResponseException(erro);
            }
        }

        [Authorize]
        // GET: api/Evento/5
        public EventoEntidade Get(int id)
        {
            try
            {
                return new EventoNegocio().Listar(id);
            }
            catch (Exception ex)
            {
                var erro = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(ex.Message),
                    ReasonPhrase = ex.Message
                };

                throw new HttpResponseException(erro);
            }
        }

        [Authorize]
        // POST: api/Evento
        public long Post([FromBody]EventoEntidade aEntidade)
        {
            try
            {
                return new EventoNegocio().Cadastrar(aEntidade);
            }
            catch (Exception ex)
            {
                var erro = new HttpResponseMessage(HttpStatusCode.NotAcceptable)
                {
                    Content = new StringContent(ex.Message)
                };

                throw new HttpResponseException(erro);
            }
        }

        [Authorize]
        // PUT: api/Evento/5
        public bool Put([FromBody]EventoEntidade aEntidade)
        {
            try
            {
                return new EventoNegocio().Editar(aEntidade);
            }
            catch (Exception ex)
            {
                var erro = new HttpResponseMessage(HttpStatusCode.NotAcceptable)
                {
                    Content = new StringContent(ex.Message)
                };

                throw new HttpResponseException(erro);
            }
        }

        [Authorize]
        // DELETE: api/Evento/5
        public bool Delete(int id)
        {
            var negocio = new EventoNegocio();
            var entidade = negocio.Listar(id);

            return negocio.Excluir(entidade);
        }

        #region .: Relatórios :.

        [Authorize]
        [Route("api/Evento/Relatorio")]
        [HttpGet]
        public IEnumerable<RelatorioEventoResponse> Relatorio()
        {
            return new EventoNegocio().Relatorio();
        }

        [Authorize]
        [Route("api/Evento/RelatorioDetalhado/{idEvento}")]
        [HttpGet]
        public RelatorioDetalhadoEventoResponse RelatorioDetalhado(long idEvento)
        {
            return new EventoNegocio().RelatorioDetalhado(idEvento);
        }

        #endregion
    }
}
