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
            var clienteNegocio = new ClienteNegocio();

            //clienteNegocio.ValidateRegister(aEntidade);

            var usuario = new UsuarioEntidade()
            {
                DataCadastro = DateTime.Now,
                DataAlteracao = DateTime.Now,
                Email = aEntidade.Email,
                Senha = StringUtilitario.GerarSenhaAlatoria(),
                Tipo = TipoUsuarioEnum.Cliente,
                IdUsuarioCadastro = aEntidade.IdUsuarioCadastro
            };

            aEntidade.IdUsuario = new UsuarioNegocio().Inserir(usuario);

            var enderecoNegocio = new EnderecoNegocio();

            aEntidade.Endereco.IdUsuarioCadastro = aEntidade.IdUsuarioCadastro;
            aEntidade.Endereco.DataCadastro = DateTime.Now;

            //enderecoNegocio.ValidateRegister(aEntidade.Endereco);

            aEntidade.IdEndereco = enderecoNegocio.Inserir(aEntidade.Endereco);

            aEntidade.DataCadastro = DateTime.Now;

            return clienteNegocio.Inserir(aEntidade);
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
