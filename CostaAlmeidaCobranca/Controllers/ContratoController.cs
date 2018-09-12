﻿using Entidade;
using Enumerador;
using Negocio;
using Projecao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Cors;
using Utilitario;

namespace CostaAlmeidaCobranca.Controllers
{
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
            var negocio = new ContratoNegocio();

            if (!negocio.ValidarCadastroContrato(aEntidade))
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            aEntidade.DataCadastro = DateTime.Now;
            aEntidade.Status = StatusContratoEnum.Ativo;

            var idContrato = negocio.Inserir(aEntidade);

            foreach (var parcela in aEntidade.Parcelas)
            {
                parcela.IdContrato = idContrato;
                parcela.DataCadastro = DateTime.Now;
                parcela.Status = StatusParcelaEnum.Pendente;

                new ParcelaNegocio().Inserir(parcela);
            }

            return idContrato;
        }

        [Authorize]
        // PUT: api/Contrato/5
        public bool Put([FromBody]ContratoEntidade aEntidade)
        {
            var negocioParcela = new ParcelaNegocio();

            foreach (var parcela in aEntidade.Parcelas)
            {
                parcela.IdContrato = aEntidade.Id;

                negocioParcela.Atualizar(parcela);
            }

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
        public GetCombosCadastroContratoResponse GetCombosCadastroContrato()
        {
            return new ContratoNegocio().getCombosCadastroContrato();
        }

        [Authorize]
        [Route("api/Contrato/Relatorio")]
        [HttpGet]
        public IEnumerable<RelatorioContratoResponse> Relatorio()
        {
            var lista = new ContratoNegocio().ListarTodosCompleto();

            return lista.Select(x => new RelatorioContratoResponse()
            {
                Id = x.Id.ToString(),
                Comprador = x.Comprador.Nome,
                Vendedor = x.Vendedor.Nome,
                Evento = x.Evento != null ? x.Evento.Nome : string.Empty,
                Valor = StringUtilitario.ValorReais(x.Valor),
                Status = new ContratoNegocio().TraduzirStatus(x.Status),
                Parcelas = x.Parcelas.Count.ToString()
            });
        }
    }
}
