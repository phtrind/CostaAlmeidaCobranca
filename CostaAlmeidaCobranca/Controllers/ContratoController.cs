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
        [Authorize]
        // GET: api/Contrato
        public IEnumerable<ContratoEntidade> Get()
        {
            return new ContratoNegocio().ListarTodosCompleto();
        }

        [Authorize]
        // GET: api/Contrato/5
        public ContratoEntidade Get(int id)
        {
            return new ContratoNegocio().ListarCompleto(id);
        }

        [Authorize]
        // POST: api/Contrato
        public long Post([FromBody]ContratoEntidade aEntidade)
        {
            aEntidade.DataCadastro = DateTime.Now;

            return new ContratoNegocio().Inserir(aEntidade);
        }

        [Authorize]
        // PUT: api/Contrato/5
        public bool Put([FromBody]ContratoEntidade aEntidade)
        {
            return new ContratoNegocio().Atualizar(aEntidade);
        }

        [Authorize]
        // DELETE: api/Contrato/5
        public bool Delete(int id)
        {
            var negocio = new ContratoNegocio();
            var entidade = negocio.Listar(id);

            return negocio.Excluir(entidade);
        }

        [Authorize]
        [Route("api/Contrato/getCombosCadastro")]
        [HttpGet]
        public getCombosCadastroContratoResponse getCombosCadastroContrato()
        {
            return new ContratoNegocio().getCombosCadastroContrato();
        }
    }
}
