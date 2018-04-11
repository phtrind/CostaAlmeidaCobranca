using Enumerador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidade
{
    public class ContratoEntidade : EntidadeBase
    {
        public long Codigo { get; set; }
        public decimal Valor { get; set; }
        public decimal TaxaLucro { get; set; }
        public string Observacao { get; set; }
        public DateTime Data { get; set; }
        public StatusContratoEnum Status { get; set; }
        public EventoEntidade Evento { get; set; }
        public ClienteEntidade Vendedor { get; set; }
        public ClienteEntidade Comprador { get; set; }
        public List<ParcelasEntidade> Parcelas { get; set; }
    }
}