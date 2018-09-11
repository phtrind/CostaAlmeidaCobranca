using Enumerador;
using System;

namespace Entidade
{
    public class FinanceiroEntidade : EntidadeBase
    {
        public long? Id { get; set; }

        public decimal Valor { get; set; }
        public TipoFinanceiroEnum Tipo { get; set; }
        public DateTime Data { get; set; }
        public StatusFinanceiroEnum Status { get; set; }

        public DateTime? DataCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }

        public long? IdUsuarioCadastro { get; set; }
        public long? IdUsuarioAlteracao { get; set; }

        public UsuarioEntidade UsuarioCadastro { get; set; }
        public UsuarioEntidade UsuarioAlteracao { get; set; }
    }
}
