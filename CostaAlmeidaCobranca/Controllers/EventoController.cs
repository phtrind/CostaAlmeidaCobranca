using Entidade;
using Negocio;
using Projecao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Transactions;
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
            using (var transation = new TransactionScope())
            {
                try
                {
                    #region .: Endereço :.

                    aEntidade.Endereco.IdUsuarioCadastro = aEntidade.IdUsuarioCadastro;
                    aEntidade.Endereco.DataCadastro = DateTime.Now;

                    aEntidade.IdEndereco = new EnderecoNegocio().Inserir(aEntidade.Endereco);

                    #endregion

                    #region .: Evento :.

                    aEntidade.DataCadastro = DateTime.Now;

                    var codEndereco = new EventoNegocio().Inserir(aEntidade);

                    #endregion

                    transation.Complete();

                    return codEndereco;
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

        #region .: Relatórios :.

        //[Authorize]
        //[Route("api/Evento/Relatorio")]
        //[HttpGet]
        //public IEnumerable<RelatorioEventoResponse> Relatorio()
        //{
        //    return new EventoNegocio().Relatorio();
        //}

        #endregion
    }
}
