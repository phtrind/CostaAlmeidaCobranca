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
    public class ParcelaController : ApiController
    {
        [Authorize]
        // GET: api/Parcela
        public IEnumerable<ParcelasEntidade> Get()
        {
            try
            {
                return new ParcelaNegocio().ListarTodos();
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
        // GET: api/Parcela/5
        public ParcelasEntidade Get(int id)
        {
            try
            {
                return new ParcelaNegocio().Listar(id);
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
        // POST: api/Parcela
        public long Post([FromBody]ParcelasEntidade aEntidade)
        {
            try
            {
                aEntidade.DataCadastro = DateTime.Now;

                return new ParcelaNegocio().Inserir(aEntidade);
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
        // PUT: api/Parcela/5
        public bool Put([FromBody]ParcelasEntidade aEntidade)
        {
            try
            {
                return new ParcelaNegocio().Editar(aEntidade);
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
        // DELETE: api/Parcela/5
        public bool Delete(int id)
        {
            try
            {
                var negocio = new ParcelaNegocio();

                return negocio.Deletar(id);
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
        [Route("api/Parcelas/RelatorioPorContrato/{id}")]
        [HttpGet]
        public IEnumerable<RelatorioParcelaPorContrato> RelatorioPorContrato(int id)
        {
            try
            {
                return new ParcelaNegocio().RelatorioPorContrato(id);
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
        [Route("api/Parcelas/BuscarParaEditar/{id}")]
        [HttpGet]
        public BuscarParaEditarParcelaResponse BuscarParaEditar(int id)
        {
            try
            {
                return new ParcelaNegocio().BuscarParaEditar(id);
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
    }
}
