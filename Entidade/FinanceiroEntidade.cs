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

        public long? IdUsuario { get; set; }

        public UsuarioEntidade Usuario { get; set; }
    }
}
