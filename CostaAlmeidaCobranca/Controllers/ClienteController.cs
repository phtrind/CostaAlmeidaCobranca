using Entidade;
using Enumerador;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
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
            return new ClienteNegocio().ListarTodos().OrderBy(x => x.Nome);
        }

        [Authorize]
        // GET: api/Cliente/5
        public ClienteEntidade Get(int id)
        {
            return new ClienteNegocio().Listar(id);
        }

        [Authorize]
        // POST: api/Cliente
        public long Post([FromBody]ClienteEntidade aEntidade)
        {
            var usuario = new UsuarioEntidade()
            {
                DataCadastro = DateTime.Now,
                DataAlteracao = DateTime.Now,
                Email = aEntidade.Email,
                Senha = StringUtilitario.GerarSenhaAlatoria(),
                Tipo = TipoUsuarioEnum.Cliente,
                IdUsuarioAlteracao = aEntidade.IdUsuarioAlteracao
            };

            aEntidade.IdUsuario = new UsuarioNegocio().Inserir(usuario);

            aEntidade.IdEndereco = new EnderecoNegocio().Inserir(aEntidade.Endereco);

            aEntidade.Endereco.DataCadastro = DateTime.Now;
            aEntidade.Endereco.DataAlteracao = DateTime.Now;

            return new ClienteNegocio().Inserir(aEntidade);
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
    }
}
