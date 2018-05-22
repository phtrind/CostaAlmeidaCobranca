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
    public class ParcelaController : ApiController
    {
        // GET: api/Parcela
        public IEnumerable<ParcelasEntidade> Get()
        {
            return new ParcelaNegocio().ListarTodosCompleto();
        }

        // GET: api/Parcela/5
        public ParcelasEntidade Get(int id)
        {
            return new ParcelaNegocio().ListarCompleto(id);
        }

        // POST: api/Parcela
        public long Post([FromBody]ParcelasEntidade aEntidade)
        {
            aEntidade.DataCadastro = DateTime.Now;

            return new ParcelaNegocio().Inserir(aEntidade);
        }

        // PUT: api/Parcela/5
        public bool Put([FromBody]ParcelasEntidade aEntidade)
        {
            return new ParcelaNegocio().Atualizar(aEntidade);
        }

        // DELETE: api/Parcela/5
        public bool Delete(int id)
        {
            var negocio = new ParcelaNegocio();
            var entidade = negocio.Listar(id);

            return negocio.Excluir(entidade);
        }
    }
}
