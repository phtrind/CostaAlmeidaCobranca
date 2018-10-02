using Entidade;
using Negocio;
using Projecao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CostaAlmeidaCobranca.Controllers
{
    [Authorize]
    public class ClienteController : ApiController
    {
        // GET: api/Cliente
        public IEnumerable<ClienteEntidade> Get()
        {
            try
            {
                return new ClienteNegocio().ListarTodos().OrderBy(x => x.Nome);
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

        // GET: api/Cliente/5
        public ClienteEntidade Get(int id)
        {
            try
            {
                return new ClienteNegocio().Listar(id);
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

        // POST: api/Cliente
        public long Post([FromBody]ClienteEntidade aEntidade)
        {
            try
            {
                return new ClienteNegocio().Cadastrar(aEntidade);
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

        // PUT: api/Cliente/5
        public bool Put([FromBody]ClienteEntidade aEntidade)
        {
            try
            {
                return new ClienteNegocio().Editar(aEntidade);
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

        // DELETE: api/Cliente/5
        public bool Delete(int id)
        {
            var negocio = new ClienteNegocio();
            var entidade = negocio.Listar(id);

            return negocio.Excluir(entidade);
        }

        #region .: Relatórios :.

        [Route("api/Cliente/Relatorio")]
        [HttpGet]
        public IEnumerable<RelatorioClienteResponse> Relatorio()
        {
            try
            {
                return new ClienteNegocio().Relatorio();
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

        [Route("api/Cliente/RelatorioDetalhado/{idCliente}")]
        [HttpGet]
        public RelatorioDetalhadoClienteResponse RelatorioDetalhado(long idCliente)
        {
            try
            {
                return new ClienteNegocio().RelatorioDetalhado(idCliente);
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

        #endregion
    }
}
