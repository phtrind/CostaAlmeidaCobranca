using System;

namespace Entidade
{
    public class CrmEntidade : EntidadeBase
    {
        public long? Id { get; set; }
        public string Descricao { get; set; }

        public DateTime? DataCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }

        public long? IdContrato { get; set; }
        public long? IdUsuarioCadastro { get; set; }
        public long? IdUsuarioAlteracao { get; set; }

        public ContratoEntidade Contrato { get; set; }
        public UsuarioEntidade UsuarioCadastro { get; set; }
        public UsuarioEntidade UsuarioAlteracao { get; set; }
    }
}
