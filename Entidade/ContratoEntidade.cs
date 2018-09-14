using Enumerador;
using System;
using System.Collections.Generic;

namespace Entidade
{
    public class ContratoEntidade : EntidadeBase
    {
        public long? Id { get; set; }

        public decimal Valor { get; set; }
        public string Animal { get; set; }
        public string Observacao { get; set; }
        public StatusContratoEnum Status { get; set; }

        public DateTime? DataCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }

        public long? IdVendedor { get; set; }
        public long? IdComprador { get; set; }
        public long? IdEvento { get; set; }
        public long? IdUsuarioCadastro { get; set; }
        public long? IdUsuarioAlteracao { get; set; }

        public ClienteEntidade Vendedor { get; set; }
        public ClienteEntidade Comprador { get; set; }
        public EventoEntidade Evento { get; set; }
        public UsuarioEntidade UsuarioCadastro { get; set; }
        public UsuarioEntidade UsuarioAlteracao { get; set; }

        public List<ParcelasEntidade> Parcelas { get; set; }
    }
}