using Entidade;
using Enumerador;
using Negocio;
using Projecao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Transactions;
using System.Web.Http;
using Utilitario;

namespace CostaAlmeidaCobranca.Controllers
{
    public class ClienteController : ApiController
    {
        [Authorize]
        // GET: api/Cliente
        public IEnumerable<ClienteEntidade> Get()
        {
            try
            {
                return new ClienteNegocio().ListarTodos().OrderBy(x => x.Nome);
            }
            catch (Exception ex)
            {
                var erro = new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(ex.Message),
                    ReasonPhrase = ex.Message
                };

                throw new HttpResponseException(erro);
            }
        }

        [Authorize]
        // GET: api/Cliente/5
        public ClienteEntidade Get(int id)
        {
            try
            {
                return new ClienteNegocio().Listar(id);
            }
            catch (Exception ex)
            {
                var erro = new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(ex.Message),
                    ReasonPhrase = ex.Message
                };

                throw new HttpResponseException(erro);
            }
        }

        [Authorize]
        // POST: api/Cliente
        public long Post([FromBody]ClienteEntidade aEntidade)
        {
            using (var transation = new TransactionScope())
            {
                try
                {
                    #region .: Usuário :.

                    var usuario = new UsuarioEntidade()
                    {
                        DataCadastro = DateTime.Now,
                        Email = aEntidade.Email,
                        Senha = StringUtilitario.GerarSenhaAlatoria(),
                        Tipo = TipoUsuarioEnum.Cliente,
                        IdUsuarioCadastro = aEntidade.IdUsuarioCadastro
                    };

                    aEntidade.IdUsuario = new UsuarioNegocio().Inserir(usuario);

                    #endregion

                    #region .: Endereço :.

                    aEntidade.Endereco.IdUsuarioCadastro = aEntidade.IdUsuarioCadastro;
                    aEntidade.Endereco.DataCadastro = DateTime.Now;

                    aEntidade.IdEndereco = new EnderecoNegocio().Inserir(aEntidade.Endereco);

                    #endregion

                    #region .: Cliente :.

                    aEntidade.DataCadastro = DateTime.Now;

                    var codCliente = new ClienteNegocio().Inserir(aEntidade); 

                    #endregion

                    transation.Complete();

                    return codCliente;
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
        // PUT: api/Cliente/5
        public bool Put([FromBody]ClienteEntidade aEntidade)
        {
            aEntidade.DataAlteracao = DateTime.Now;

            return new ClienteNegocio().Atualizar(aEntidade);
        }

        [Authorize]
        // DELETE: api/Cliente/5
        public bool Delete(int id)
        {
            var negocio = new ClienteNegocio();
            var entidade = negocio.Listar(id);

            return negocio.Excluir(entidade);
        }

        #region .: Relatórios :.

        [Authorize]
        [Route("api/Cliente/Relatorio")]
        [HttpGet]
        public IEnumerable<RelatorioClienteResponse> Relatorio()
        {
            return new ClienteNegocio().Relatorio();
        }

        [Authorize]
        [Route("api/Cliente/Relatorio/{idCliente}")]
        [HttpGet]
        public IEnumerable<RelatorioClienteResponse> Relatorio(long idCliente)
        {
            return new ClienteNegocio().Relatorio(idCliente);
        }

        #endregion
    }
}
