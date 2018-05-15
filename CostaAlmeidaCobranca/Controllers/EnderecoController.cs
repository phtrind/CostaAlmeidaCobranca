﻿using Entidade;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CostaAlmeidaCobranca.Controllers
{
    public class EnderecoController : ApiController
    {
        // GET: api/Endereco
        public IEnumerable<EnderecoEntidade> Get()
        {
            return new EnderecoNegocio().ListarTodos();
        }

        // GET: api/Endereco/5
        public EnderecoEntidade Get(int aId)
        {
            return new EnderecoNegocio().Listar(aId);
        }

        // POST: api/Endereco
        public long Post([FromBody]EnderecoEntidade aEntidade)
        {
            aEntidade.DataCadastro = DateTime.Now;

            return new EnderecoNegocio().Inserir(aEntidade);
        }

        // PUT: api/Endereco/5
        public bool Put([FromBody]EnderecoEntidade aEntidade)
        {
            return new EnderecoNegocio().Atualizar(aEntidade);
        }

        // DELETE: api/Endereco/5
        public bool Delete(int id)
        {
            var negocio = new EnderecoNegocio();
            var entidade = negocio.Listar(id);

            return negocio.Excluir(entidade);
        }
    }
}