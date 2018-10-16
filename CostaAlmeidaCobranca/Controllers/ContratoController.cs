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
    public class ContratoController : ApiController
    {
        #region .: Busca :.

        [Authorize]
        // GET: api/Contrato
        public IEnumerable<ContratoEntidade> Get()
        {
            try
            {
                return new ContratoNegocio().ListarTodos().OrderByDescending(x => x.DataCadastro);
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
        // GET: api/Contrato/5
        public ContratoEntidade Get(int id)
        {
            try
            {
                return new ContratoNegocio().Listar(id);
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
        [Route("api/Contrato/BuscarParaEditar/{idContrato}")]
        [HttpGet]
        public BuscarParaEditarContratoResponse BuscarParaEditar(long idContrato)
        {
            try
            {
                return new ContratoNegocio().BuscarParaEditar(idContrato);
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

        [Authorize]
        // POST: api/Contrato
        public long Post([FromBody]ContratoEntidade aEntidade)
        {
            try
            {
                return new ContratoNegocio().Cadastrar(aEntidade);
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
        // PUT: api/Contrato/5
        public bool Put([FromBody]ContratoEntidade aEntidade)
        {
            try
            {
                return new ContratoNegocio().Editar(aEntidade);
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
        // DELETE: api/Contrato/5
        public bool Delete(int id)
        {
            try
            {
                return new ContratoNegocio().Deletar(id);
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
        [Route("api/Contrato/getCombosCadastro")]
        [HttpGet]
        public GetCombosCadastroContratoResponse GetCombosCadastroContrato()
        {
            try
            {
                return new ContratoNegocio().getCombosCadastroContrato();
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

        #region .: Relatório :.

        [Authorize]
        [Route("api/Contrato/Relatorio")]
        [HttpGet]
        public IEnumerable<RelatorioContratoResponse> Relatorio()
        {
            return new ContratoNegocio().Relatorio();
        }

        #endregion
    }
}
