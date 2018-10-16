using Entidade;
using Negocio;
using Projecao;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CostaAlmeidaCobranca.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [Authorize]
    public class CrmController : ApiController
    {
        #region .: Métodos Inutilizados :.

        //// GET: api/Crm
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET: api/Crm/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// PUT: api/Crm/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/Crm/5
        //public void Delete(int id)
        //{
        //} 

        #endregion

        // POST: api/Crm
        public long Post([FromBody]CrmEntidade aEntidade)
        {
            try
            {
                return new CrmNegocio().Cadastrar(aEntidade);
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

        [Route("api/Crm/Contrato/{id}")]
        [HttpGet]
        public IEnumerable<CrmProjecao> Relatorio(long id)
        {
            try
            {
                return new CrmNegocio().BuscarPorContrato(id);
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
