using Enumerador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidade
{
    public class ContratoEntidade : EntidadeBase
    {
        public long Id { get; set; }
        public DateTime DataCadastro { get; set; }

        public decimal Valor { get; set; }
        public decimal TaxaLucro { get; set; }
        public string Observacao { get; set; }
        public StatusContratoEnum Status { get; set; }

        public long? IdEvento { get; set; }
        public long? IdVendedor { get; set; }
        public long? IdComprador { get; set; }
        public long? IdUsuario { get; set; }

        public EventoEntidade Evento { get; set; }
        public ClienteEntidade Vendedor { get; set; }
        public ClienteEntidade Comprador { get; set; }
        public UsuarioEntidade Usuario { get; set; }
        public List<ParcelasEntidade> Parcelas { get; set; }
    }
}